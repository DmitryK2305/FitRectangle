using FitRectangle.Model;

namespace FitRectangle.Test
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(0, 1, 1, 0)]
        public void Creation_TopLeftBotRight_Ok(double topLeftX, double topLeftY, double botRightX, double botRightY)
        {
            Point topLeft = new Point(topLeftX, topLeftY);
            Point botRight = new Point(botRightX, botRightY);

            Rectangle rectangle = new Rectangle(topLeft, botRight);
            
            Assert.AreEqual(topLeftX, rectangle.TopLeft.X);
            Assert.AreEqual(topLeftX, rectangle.BotLeft.X);
            Assert.AreEqual(topLeftY, rectangle.TopLeft.Y);
            Assert.AreEqual(topLeftY, rectangle.TopRight.Y);
            Assert.AreEqual(botRightX, rectangle.BotRight.X);
            Assert.AreEqual(botRightX, rectangle.TopRight.X);
            Assert.AreEqual(botRightY, rectangle.BotRight.Y);
            Assert.AreEqual(botRightY, rectangle.BotLeft.Y);
        }
        
        [TestMethod]
        [DataRow(0, 1, 1, 0)]
        [DataRow(0, 2, 2, 0)]
        [DataRow(0, 3.14, 3.14, 0)]
        public void Change_TopLeftBotRight_Ok(double topLeftX, double topLeftY, double botRightX, double botRightY)
        {
            Point topLeft = new Point(topLeftX, topLeftY);
            Point botRight = new Point(botRightX, botRightY);

            Rectangle rectangle = new Rectangle(new Point(0, 0), new Point(0,0));
            rectangle.Change(topLeft, botRight);
            
            Assert.AreEqual(topLeftX, rectangle.TopLeft.X);
            Assert.AreEqual(topLeftX, rectangle.BotLeft.X);
            Assert.AreEqual(topLeftY, rectangle.TopLeft.Y);
            Assert.AreEqual(topLeftY, rectangle.TopRight.Y);
            Assert.AreEqual(botRightX, rectangle.BotRight.X);
            Assert.AreEqual(botRightX, rectangle.TopRight.X);
            Assert.AreEqual(botRightY, rectangle.BotRight.Y);
            Assert.AreEqual(botRightY, rectangle.BotLeft.Y);
        }

        [TestMethod]
        [DataRow(0, 4, 4, 0, 1, 2)]
        public void IsPointIn_True(double mainTopLeftX, double mainTopLeftY, double mainBotRightX, double mainBotRightY, double pointX, double pointY)
        {
            Point mainTopLeft = new Point(mainTopLeftX, mainTopLeftY);
            Point mainBotRight = new Point(mainBotRightX, mainBotRightY);

            Rectangle main = new Rectangle(mainTopLeft, mainBotRight);

            bool result = main.IsPointIn(new Point(pointX, pointY));

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        [DataRow(0, 4, 4, 0, 5, 1)]
        public void IsPointIn_False(double mainTopLeftX, double mainTopLeftY, double mainBotRightX, double mainBotRightY, double pointX, double pointY)
        {
            Point mainTopLeft = new Point(mainTopLeftX, mainTopLeftY);
            Point mainBotRight = new Point(mainBotRightX, mainBotRightY);

            Rectangle main = new Rectangle(mainTopLeft, mainBotRight);

            bool result = main.IsPointIn(new Point(pointX, pointY));

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [DataRow(0, 4, 4, 0, 1, 2, 2, 1)]
        public void Clip_Full_Ok(double mainTopLeftX, double mainTopLeftY, double mainBotRightX, double mainBotRightY, double innerTopLeftX, double innerTopLeftY, double innerBotRightX, double innerBotRightY)
        {
            Point mainTopLeft = new Point(mainTopLeftX, mainTopLeftY);
            Point mainBotRight = new Point(mainBotRightX, mainBotRightY);
            
            Point innerTopLeft = new Point(innerTopLeftX, innerTopLeftY);
            Point innerBotRight = new Point(innerBotRightX, innerBotRightY);

            Rectangle main = new Rectangle(mainTopLeft, mainBotRight);
            Rectangle inner = new Rectangle(innerTopLeft, innerBotRight);

            ClippingResult result = inner.Clip(main);

            Assert.IsFalse(result.Clipped);
            Assert.AreEqual(4, result.Points.Length);
        }
        
        [TestMethod]
        [DataRow(0, 4, 4, 0, 1, 2, 5, 1)]
        public void Clip_Half_Ok(double mainTopLeftX, double mainTopLeftY, double mainBotRightX, double mainBotRightY, double innerTopLeftX, double innerTopLeftY, double innerBotRightX, double innerBotRightY)
        {
            Point mainTopLeft = new Point(mainTopLeftX, mainTopLeftY);
            Point mainBotRight = new Point(mainBotRightX, mainBotRightY);
            
            Point innerTopLeft = new Point(innerTopLeftX, innerTopLeftY);
            Point innerBotRight = new Point(innerBotRightX, innerBotRightY);

            Rectangle main = new Rectangle(mainTopLeft, mainBotRight);
            Rectangle inner = new Rectangle(innerTopLeft, innerBotRight);

            ClippingResult result = inner.Clip(main);

            Assert.IsTrue(result.Clipped);
            Assert.AreEqual(2, result.Points.Length);
        }
        
        [TestMethod]
        [DataRow(0, 4, 4, 0, 1, 2, 5, -1)]
        public void Clip_Quater_Ok(double mainTopLeftX, double mainTopLeftY, double mainBotRightX, double mainBotRightY, double innerTopLeftX, double innerTopLeftY, double innerBotRightX, double innerBotRightY)
        {
            Point mainTopLeft = new Point(mainTopLeftX, mainTopLeftY);
            Point mainBotRight = new Point(mainBotRightX, mainBotRightY);
            
            Point innerTopLeft = new Point(innerTopLeftX, innerTopLeftY);
            Point innerBotRight = new Point(innerBotRightX, innerBotRightY);

            Rectangle main = new Rectangle(mainTopLeft, mainBotRight);
            Rectangle inner = new Rectangle(innerTopLeft, innerBotRight);

            ClippingResult result = inner.Clip(main);

            Assert.IsTrue(result.Clipped);
            Assert.AreEqual(1, result.Points.Length);
        }
        
        [TestMethod]
        [DataRow(0, 4, 4, 0, 0, -1, 4, -5)]
        public void Clip_None_Ok(double mainTopLeftX, double mainTopLeftY, double mainBotRightX, double mainBotRightY, double innerTopLeftX, double innerTopLeftY, double innerBotRightX, double innerBotRightY)
        {
            Point mainTopLeft = new Point(mainTopLeftX, mainTopLeftY);
            Point mainBotRight = new Point(mainBotRightX, mainBotRightY);
            
            Point innerTopLeft = new Point(innerTopLeftX, innerTopLeftY);
            Point innerBotRight = new Point(innerBotRightX, innerBotRightY);

            Rectangle main = new Rectangle(mainTopLeft, mainBotRight);
            Rectangle inner = new Rectangle(innerTopLeft, innerBotRight);

            ClippingResult result = inner.Clip(main);

            Assert.IsTrue(result.Clipped);
            Assert.AreEqual(0, result.Points.Length);
        }
    }
}