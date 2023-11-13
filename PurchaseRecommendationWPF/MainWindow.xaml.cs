using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.Recommender;
using Microsoft.ML.Transforms;
using Microsoft.Win32;
using SalesDataModel;
using SalesDataAccess;
using static Microsoft.ML.DataOperationsCatalog;

namespace PurchaseRecommendationWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private MLContext mlContext;
        private TrainTestData trainTestData;
        private MatrixFactorizationTrainer.Options options;
        private MatrixFactorizationTrainer trainer;
        private ITransformer trainedModel;

        private void btnStep1_Click(object sender, RoutedEventArgs e)
        {
            //Step 1: Create MLContext to be shared across the model
            //Creation workflow objects
            mlContext = new MLContext();
            lblStep1Status.Content = "MLContext is created successful";
        }

        private void btnStep2_Click(object sender, RoutedEventArgs e)
        {
            //Step 2: Read the training data which will be used to train the movie recommendation model
            //The schema for training data is defined by type 'TInput' in LoadFromTextFile<TInput>() method
            List<DataEntry> dataEntryList = new DataEntryConnector().GetAllDataEntries();
            /*      IDataView dataView = mlContext.Data.LoadFromTextFile<MovieRating>(
                      RatingRelativePath, hasHeader: true, separatorChar: ',');*/
            IDataView dataView = mlContext.Data.LoadFromEnumerable(dataEntryList);

            double trainRate = double.Parse(txtTrainRate.Text) / 100;
            double testRate = 1 - trainRate;

            trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction: testRate);

            lblStep2Status.Content = "Create Train test data successful!";
        }

        private void btnStep3_Click(object sender, RoutedEventArgs e)
        {
            //STEP 3: Your data is already encoded so all you need to do is specify options 
            // for MatrxiFactorizationTrainer with a few extra hyperparameters
            // LossFunction, Alpa, Lambda and a few others like H and C as shown below and call the trainer.

            options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = nameof(DataEntry.CustomerID);
            options.MatrixRowIndexColumnName = nameof(DataEntry.ProductID);
            options.LabelColumnName = "Label";
            options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
            options.Alpha = 0.01;
            options.Lambda = 0.025;
            options.C = 0.00001;
            lblStep3Status.Content = "Configuration Matrix Factorization Trainer successful!";
        }

        private void btnStep4_Click(object sender, RoutedEventArgs e)
        {
            //Step 4: Call the MatrixFactorization trainer by passing options.
            trainer = mlContext.Recommendation().Trainers.MatrixFactorization(options);
            lblStep4Status.Content = "Create Matrixfactorization trainer successful!";
        }
        private void btnStep5_Click(object sender, RoutedEventArgs e)
        {
            //Step 5: Train the model fitting to the Dataset
            trainedModel = trainer.Fit(trainTestData.TrainSet);
            lblStep5Status.Content = "Trained model successful!";
        }

        private void btnStep6_Click(object sender, RoutedEventArgs e)
        {
            //Step 6: Evaluate the model performance
            IDataView prediction = trainedModel.Transform(trainTestData.TestSet);

            RegressionMetrics metrics = mlContext.Regression.Evaluate(
                prediction, labelColumnName: "Label", scoreColumnName: "Score");
            txtLossFunction.Text = metrics.LossFunction.ToString();
            txtMAE.Text = metrics.MeanAbsoluteError.ToString();
            txtMSE.Text = metrics.MeanSquaredError.ToString();
            txtRMSE.Text = metrics.RootMeanSquaredError.ToString();
        }

        private void btnStep7_Click(object sender, RoutedEventArgs e)
        {
            //Step 7: Test a single prediction by predicting a single rating for a specific user
            var predictionengine = mlContext.Model.CreatePredictionEngine
                <DataEntry, DataPrediction>(trainedModel);

            uint customerId = uint.Parse(txtCustomerId.Text);
            uint productId = uint.Parse(txtProductId.Text);
            var prediction = predictionengine.Predict(
                new DataEntry()
                {
                    CustomerID = customerId,
                    ProductID = productId
                });
            FlowDocument mcFlowDoc = new FlowDocument();
            Paragraph para = new Paragraph();
            para.Inlines.Add(new Run("For Customer ID= "));
            para.Inlines.Add(new Bold(new Run(customerId + " ")));
            para.Inlines.Add(new Run(" and Product ID= "));
            para.Inlines.Add(new Bold(new Run(productId + " ")));
            para.Inlines.Add(new Run(" the predicted score = "));
            para.Inlines.Add(new Bold(new Run(Math.Round(prediction.Score, 1) + "")));
            mcFlowDoc.Blocks.Add(para);
            txtResult.Document = mcFlowDoc;
        }

        private void btnStep81_Click(object sender, RoutedEventArgs e)
        {
            //Step 8.1: Save Model
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "File .zip|*.zip";
            if (saveFileDialog.ShowDialog() == true)
            {
                mlContext.Model.Save(
                    trainedModel,
                    trainTestData.TrainSet.Schema,
                    saveFileDialog.FileName);
                MessageBox.Show("Save trained model successful!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnStep82_Click(object sender, RoutedEventArgs e)
        {
            //Step 8.2: Load Model
            DataViewSchema modelSchema;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File .zip|*.zip";
            if (openFileDialog.ShowDialog() == true)
            {
                if (mlContext == null)
                    mlContext = new MLContext();
                //Load trained model
                trainedModel = mlContext.Model.Load(
                    openFileDialog.FileName, out modelSchema);
                MessageBox.Show("Load trained model successful!",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnPredictionForAll_Click(object sender, RoutedEventArgs e)
        {
            ProductRecommendationWindow prw = new ProductRecommendationWindow();
            prw.Show();
        }
    }
}
