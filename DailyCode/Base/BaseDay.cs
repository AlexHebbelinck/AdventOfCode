using Common.Helpers;
using Common.Models;

namespace DailyCode.Base
{
    public abstract class BaseDay
    {
        private readonly string _sessionId;

        protected BaseDay(string sessionId)
        {
            _sessionId = sessionId;
        }

        public async Task<string> Run(AdventConfig config)
        {
            var fileInput = config.UseTestData
                ? await InputHelper.Instance.GetTestData()
                : await InputHelper.Instance.GetInputData(GetType().Name, _sessionId, config);

            SetupData(fileInput);

            return config.Part switch
            {
                1 => await Task.Run(() => RunPart1()),
                2 => await Task.Run(() => RunPart2()),
                _ => throw new Exception("Part doesn't exist"),
            };
        }

        protected abstract void SetupData(List<string> fileInput);

        protected abstract string RunPart1();

        protected abstract string RunPart2();
    }
}