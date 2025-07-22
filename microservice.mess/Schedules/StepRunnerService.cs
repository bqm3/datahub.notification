using microservice.mess.Models.Message;
using microservice.mess.Models;
using microservice.mess.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace microservice.mess.Schedules
{
    public class StepRunnerService
    {
        private readonly ResolveScheduleFieldStep _resolveStep;

        public StepRunnerService(ResolveScheduleFieldStep resolveStep)
        {
            _resolveStep = resolveStep;
        }

        public async Task RunStepsAsync(ScheduleContext context)
        {
            await _resolveStep.ExecuteAsync(context);
        }
        // private readonly Dictionary<string, IMessageStep> _stepMap;

        // public StepRunnerService(IEnumerable<IMessageStep> steps)
        // {
        //     _stepMap = new Dictionary<string, IMessageStep>(StringComparer.OrdinalIgnoreCase);

        //     foreach (var step in steps)
        //     {
        //         switch (step)
        //         {
        //             case QueryDataStep s: _stepMap["query_data"] = s; break;
        //             case FormatDataSignetStep s: _stepMap["format_data_signet"] = s; break;
        //             case FormatDataMailStep s: _stepMap["format_data_mail"] = s; break;
        //             case GeneratePdfStep s: _stepMap["generate_pdf"] = s; break;
        //             // case UploadSignetStep s: _stepMap["upload_signet"] = s; break;
        //             case GenerateHashStep s: _stepMap["generate_hash"] = s; break;
        //             case SendToSignetStep s: _stepMap["send_to_signet"] = s; break;
        //             case SendToMailStep s: _stepMap["send_to_mail"] = s; break;
        //             // case SendToSignetStep s: _stepMap["send_to_mail"] = s; break;
        //                 // thêm các step khác nếu có
        //         }
        //     }
        // }

        // public async Task RunStepsAsync(List<string> steps, ScheduleContext context)
        // {
        //     foreach (var step in steps)
        //     {
        //         if (_stepMap.TryGetValue(step.ToLower(), out var handler))
        //         {
        //             await handler.ExecuteAsync(context);
        //         }
        //         else
        //         {
        //             throw new Exception($"Step '{step}' not registered.");
        //         }
        //     }
        // }
    }


}