using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;

namespace Bootcamp2015.AmazingRace.Base
{
    public class FakeIoC
    {
        private static FakeIoC fakeIoC;
        public static FakeIoC GetInstance()
        {
            if(fakeIoC == null)
                fakeIoC = new FakeIoC();
            return fakeIoC;
        }

        private FakeIoC()
        {
            
        }

        public IDataService DataService { get; set; }
        public ISettingsService SettingsService { get; set; }
    }
}