//namespace ExperimentalGoal.AI_Images
//{
//    using System;
//    using System.Drawing;
//    using System.Drawing.Imaging;
//    using System.IO;
//    using System.Runtime.InteropServices;
//    using TensorFlow;

//    public class ImageGenerator
//    {
//        private const string ModelPath = "path/to/your/pretrained/model"; // Path to the pre-trained model
//        private const int ImageSize = 64; // Size of the generated image

//        public Bitmap GenerateImage(int age, Gender gender)
//        {
//            using (var graph = new TFGraph())
//            {
//                // Load the pre-trained model
//                var model = File.ReadAllBytes(ModelPath);
//                graph.Import(new TFBuffer(model));

//                using (var session = new TFSession(graph))
//                {
//                    // Create the input tensor
//                    var inputTensor = CreateInputTensor(age, gender);

//                    // Run the model to generate the output tensor
//                    var outputTensor = session.Run(
//                        inputs: new[] { graph["input"][0] },
//                        inputValues: new[] { inputTensor },
//                        outputs: new[] { graph["output"][0] }
//                    );

//                    // Get the generated image tensor
//                    var imageTensor = outputTensor[0];

//                    // Convert the tensor to a bitmap image
//                    var imageBytes = (byte[])imageTensor.GetValue();
//                    var bitmap = GetBitmapFromBytes(imageBytes);

//                    return bitmap;
//                }
//            }
//        }

//        private TFTensor CreateInputTensor(int age, Gender gender)
//        {
//            // Create a 2D float tensor with shape [1, 2]
//            var tensorShape = new TFShape(1, 2);
//            var tensor = new TFTensor(TFDataType.Float, tensorShape);

//            // Set the input values
//            var tensorData = new float[2];
//            tensorData[0] = age;
//            tensorData[1] = (float)gender;
//            tensor.SetValue(tensorData, tensorShape);

//            return tensor;
//        }

//        private Bitmap GetBitmapFromBytes(byte[] imageBytes)
//        {
//            // Assuming the generated image is in RGB format
//            var bitmap = new Bitmap(ImageSize, ImageSize, PixelFormat.Format24bppRgb);

//            var imageData = bitmap.LockBits(
//                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
//                ImageLockMode.WriteOnly,
//                bitmap.PixelFormat
//            );

//            Marshal.Copy(imageBytes, 0, imageData.Scan0, imageBytes.Length);

//            bitmap.UnlockBits(imageData);

//            return bitmap;
//        }
//    }

//    public enum Gender
//    {
//        Male,
//        Female
//    }

//}
