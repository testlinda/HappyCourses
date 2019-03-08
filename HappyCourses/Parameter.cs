using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyCourses
{
    public delegate void OperationHandler();

    class Parameter
    {
        public readonly static string _patternPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pattern");
        public readonly static string _patternImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pattern\\pattern.png");
        public readonly static string _screenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screenshot");
        public readonly static string _screenshotImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "screenshot\\screenshot.png");
        public readonly static string _gifPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gif");
        public readonly static string _iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.ini");
        public readonly static string _iniSectionSetting = "Setting";
        public readonly static string _iniSectionClick1 = "MouseClick1";
        public readonly static string _iniSectionClick2 = "MouseClick2";
        public readonly static string _iniKeyTimeInterval = "TimeInterval";
        public readonly static string _iniKeyTimeX = "X";
        public readonly static string _iniKeyTimeY = "Y";

        public static Screen _screen = null;
        public static int _timer_check_interval_sec = 60;
        public static int _location_Click1_X = 274;
        public static int _location_Click1_Y = 53;
        public static int _location_Click2_X = 74;
        public static int _location_Click2_Y = 48;

        public static string _patternImageInBase64 = "iVBORw0KGgoAAAANSUhEUgAAAKsAAAALCAYAAAAA9H33AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsIAAA7CARUoSoAAAAQRSURBVFhH7Zg/VBpBEIdPqqTTTkrSYacdlJTaaQcl6UxnulCSLum0CyV22mHnldhpB52U2GkXO8M33I83uXDncZBn4vN7j7d/2Nud+c3u3ELwNGFw3H06Dkqzz23vku7c3A8GT2eVetR6ObADfy7rR4n1rOR55m/wEnbkXVP7inIVFIIJ4adWsPPlMDh8ug2KlUpwsfeR7tx0t/ai2suyXi6bT7Xut6jnjTz8KzoWRhehVcbhtZX7/a4ZBufVRtApVq0OYeNzcLL2IXgYDoOr1ner60MfaPz46mpWH56czsb5+dSveVVnXbVln8fbxbqMo8+3sU915swK62ltPvPWF34cvogkbSBJC49s8LrIvzjxtWRvWuxEHh19ndI/A96ecX+6pzz6jo80y2pHYaNUtE42F52eYm0neLy7swdgFPYt88L115NZNoaw2bayOe5byTjqiOczN3jn4GE0tu/K9f1geHpu62rsTbtjpcfbNQ5vrA/7rYzam9VtKxeB+XirYIfsoS3/PWiFj4yrHbfNR8RnbJI2WbTwoDfjdns/zD8C5mE+1moMejbu3ebmTK+k2JElxTI6yjZ84Rlske/Sj7h6kjTLakcB41lQaBdDubFr5bB7YRMxYbmxF9xHRmAYJ4zFycjzuOtPF9NcW80DM0QiAnN6NBbx70cjq3skII7404tgtHmutFuLerMjYUrR+rz28M0HGJQRtltNK8uHdVtz2O2lapNFCw/fA74wv95+gn7mJz7EjfhIr6TYeZbRsVSbZsL14jTZQVw/v16aZlntsDtrpX1kp1MgNAMJEicBkWRIsbZtD9IPZEK/weP8jILHPZZxzL0ss+BNHOGEc5JpsxloS8hFeRj/ngmSSBrHRknTZlkt4geXGDHPoHNmmxYNRFLsPKvU8XE0yYwp+j2nWRY7bLMCzuEwA4HFgRTN6ecE4LyyjO62pHNA+HkZ4n10zdCrSp94tloUHGAzkDGK1R1rY4PaefBZIo2kcRulkpVJ2iyrheYXo0nWhIPwz6sSJMXOs0od0/R7TrMsdhQ49ZxOpWkJqlOo1wlO4zwwVs+QzrXB54mhFM/rCPxlehm8kNjq29iUB/msTSBtyGAeza/7ITogKq+9NG0W1YJgAetb0CL9hWLF1UNjPPNiF2eVOsb145CINM0gix0FrgAIymUXkRGIjKCNR6nXi5znYe65eoYTwY8AwXwIxHekeOZjXtr060fYMsgBbMNGCaVXcB6YBz/0+pYW+BCH7Cgf0YFx2JSmzaJaoCPj+JGHX8TKozbfa4zfsPNiF2eVOvI8/km/9egwiSTNIIsda/zZGtUT4fSTrpN+RL2xWsiSbD42fXyDLsprit3szjoPvdI4rbVOK+p943/g9cUuCH4Bx1Ib6ULrDFYAAAAASUVORK5CYII=";
    }
}
