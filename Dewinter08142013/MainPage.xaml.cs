using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using System.Windows.Input;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Brightness_Contrast
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //double brightness = 0.5;
        //double contrast = 0.5;
        double sature;
        double sharp;
        byte[] resultbyte;
        byte[] passbyte;
        double gamma;
        double amount;
        byte[] pixelColors;
        int[] bytesAsInts;
        int[] resultarray;
        bool check1,check12,check22, check2 = false;
        bool check3, check32,check4,check42 = false;

      
        

       
        
        
        conversion1 cn = new conversion1();
       
        public MainPage()
        {
            
            this.InitializeComponent();
            txtbox.Text = Convert.ToString(50);
            txtbox1.Text = Convert.ToString(50);
            txtbox3.Text = Convert.ToString(50);
            txtbox4.Text = Convert.ToString(50);
            txtbox5.Text = Convert.ToString(50);
            txtbox6.Text = Convert.ToString(50); 
            
       
         }
        

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            img.Source = new BitmapImage(new Uri("ms-appx:///Pictures/Kim.jpg", UriKind.RelativeOrAbsolute));
         String SlideValue = txtbox.Text;
        }



        Brightness_and_Contrast bg = new Brightness_and_Contrast();
       

       

        async void LoadImage(string fileName,string option)
        {
           
            // Open the file
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx://" + fileName));

            using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                // We have to load the file in a BitmapImage first, so we can check the width and height..
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(fileStream);
                // Load the picture in a WriteableBitmap
                WriteableBitmap writeableBitmap = new WriteableBitmap(bmp.PixelWidth, bmp.PixelHeight);
                writeableBitmap.SetSource(await file.OpenAsync(Windows.Storage.FileAccessMode.Read));

                // Now we have to extract the pixels from the writeablebitmap
                // Get all pixel colors from the buffer
               pixelColors = writeableBitmap.PixelBuffer.ToArray();


             //  passbyte = pixelColors;
               bytesAsInts = conversion1.GetIntArrayFromByteArray(pixelColors);

                //   txtbox.Text += 0.5;
               
                if (option == Convert.ToString(1))
                {
                    if (check12)
                    {
                        resultbyte = passbyte;

                        check12 = false;
                    }
                    bool check122 = false;
                        if (resultbyte != null && check1)
                        {
                            bytesAsInts = conversion1.GetIntArrayFromByteArray(resultbyte);
                            check122 = true;
                        }
                        else
                        {
                           
                            bytesAsInts = conversion1.GetIntArrayFromByteArray(pixelColors);
                        }
                       
                        resultarray = bg.Process(bytesAsInts, bmp.PixelWidth, bmp.PixelHeight);
                        passbyte = conversion1.GetByteArrayFromIntArray(resultarray);

                        if (check122)
                        {
                            check1 = true;
                        }
                        else
                        {
                            check1 = false;
                            
                        }
                        check2 = true;
                        check3 = true;
                        check22 = true;
                        check32 = true;
                        check4 = true;
                        check42 = true;

                    // Now we have to write back our pixel colors to the writeable bitmap..
                   
                }
                else if (option == Convert.ToString(2))
             {
                 bool check222 = false;
                 if (check22)
                 {
                     resultbyte = passbyte;

                     check22 = false;
                 }


                    if (resultbyte != null && check2)
                    {
                        Saturation st = new Saturation();
                        passbyte = st.satura(resultbyte, bmp.PixelWidth, bmp.PixelHeight, sature);
                        check222 = true;
                    }
                    else
                    {
                        Saturation st = new Saturation();
                        passbyte = st.satura(pixelColors, bmp.PixelWidth, bmp.PixelHeight, sature);
                    }

                    if (check222)
                    {
                        check2 = true;
                    }
                    else 
                    {
                        check2 = false;
                    }
                    check12 = true;
                    check32 = true;
                    check3 = true;
                    check1 = true;
                    check4 = true;
                    check42 = true;
                   
                     
                }
                else if (option == Convert.ToString(3))
                {

                    resultarray = Bitmap_Sharpen.sharpen(bytesAsInts, bmp.PixelWidth, bmp.PixelHeight, sharp);
                    passbyte = conversion1.GetByteArrayFromIntArray(resultarray);
                    resultbyte = passbyte;
                }
                else if (option == Convert.ToString(4))
                {
                    bool check333 = false;
                    
                    if (check32)
                    {
                        resultbyte = passbyte;

                        check32 = false;
                    }
                    if (resultbyte != null && check3)
                    {
                        Gamma gam = new Gamma();
                        passbyte = gam.GammaChange(resultbyte, bmp.PixelWidth, bmp.PixelHeight, gamma);
                        check333 = true;
                    }
                    else
                    {
                        check333 = false;
                        Gamma gam = new Gamma();
                        passbyte = gam.GammaChange(pixelColors, bmp.PixelWidth, bmp.PixelHeight, gamma);
                    }
                    if (check333)
                    {
                        check3 = true;
                    }
                    else
                    {
                        check3 = false;
                    }
                    
                    check12 = true;
                    check22 = true;
                    check2 = true;
                    check1 = true;
                    check4 = true;
                    check42 = true;
                }
                else if (option == Convert.ToString(5))
                {

                    //hue code
                     bool check444 = false;
                    
                    if (check42)
                    {
                        resultbyte = passbyte;

                        check42 = false;
                    }
                    if (resultbyte != null && check4 == true)
                    {
                        writeableBitmap.PixelBuffer.AsStream().Write(passbyte, 0, passbyte.Length);
                        writeableBitmap = ChangeHue.ChangeHue1(writeableBitmap, amount);
                        check444 = true;
                    }
                    else
                    {
                        writeableBitmap = ChangeHue.ChangeHue1(writeableBitmap, amount);
                    }
                    resultbyte = writeableBitmap.PixelBuffer.ToArray();
                    passbyte = resultbyte;
                    if (check444)
                    {
                        check4 = true;
                    }
                    else
                    {
                        check4 = false;
                    }

                    check12 = true;
                    check22 = true;
                    check2 = true;
                    check1 = true;
                    check3 = true;
                    check32 = true;


                }

                
               
                    writeableBitmap.PixelBuffer.AsStream().Write(passbyte, 0, passbyte.Length);
              
                    // Set the source of our image to the WriteableBitmap
                    img1.Source = writeableBitmap;
                    //img1.Source = wb;
                    // Tell the image it needs a redraw
                    writeableBitmap.Invalidate();
                    
                
            }
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            
            float hel = (float)Convert.ToDouble(txtbox.Text);
            if (hel <= 50)
            {
               hel = hel - 50;
                bg.BrightnessFactor = hel / 100;
            }
            else
            { bg.BrightnessFactor = hel / 100; }
            string option = Convert.ToString(1);
            LoadImage("/Pictures/Kim.jpg", option);

        }

        private void txtbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            float hel = (float)Convert.ToDouble(txtbox1.Text);
            if (hel <= 50)
            {
                hel = hel - 50;
                bg.ContrastFactor = hel / 100;
            }
            else
            { bg.ContrastFactor = hel / 100; }
            string option = Convert.ToString(1);
            LoadImage("/Pictures/Kim.jpg", option);
        }

        private void txtbox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            sature = Convert.ToDouble(txtbox3.Text);
            string option = Convert.ToString(2);
            LoadImage("/Pictures/Kim.jpg", option);
        }

        private void txtbox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            sharp = Convert.ToDouble(txtbox4.Text);
            string option = Convert.ToString(3);
            LoadImage("/Pictures/Kim.jpg", option);

        }

        private void txtbox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            //gamma
            gamma = Convert.ToDouble(txtbox5.Text);
            string option = Convert.ToString(4);
            LoadImage("/Pictures/Kim.jpg", option);
        }

        private void txtbox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            //hue
           amount = Convert.ToDouble(txtbox6.Text);
            string option = Convert.ToString(5);
            LoadImage("/Pictures/Kim.jpg", option);
        }
        
    }
}
