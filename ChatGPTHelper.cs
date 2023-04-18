using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace CorrectEssayV2;

public class ChatGPTHelper
{
    private const string Key = "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";

    //private readonly string statement = "请对以下作文给出评语眉批和总批，并且从审题立意、素材、结构、语言表达、技法运用等方面提出修改意见。";
    //private readonly string statement = "请对以下作文给出评语眉批和总批，并且从中心切合题目、选材典型、材料围绕中心、层次段落安排清晰合理、叙述详略得当、开头结尾相照应、语言有亮点等方面提出修改意见，";

    private const string statement =
        "我希望你假定自己是初中七年级语文老师，根据初中七年级作文评判标准，给我评分，并且按照评分细则给出打分依据，此外，请给我详细的评语眉批及总批，并且从审题立意、素材、结构、语言表达、技法运用等方面提出修改意见。" +
        "以下是我的作文：";

    private const string trail = "请依次给到我以下内容：具体分数以及评分依据、文章评语眉批、评语总批、修改意见";

    public string EssayRequirement { get; set; } = string.Empty;

    private string Prompt => $"{EssayRequirement ?? string.Empty}{statement}";

    public async Task<string> Chat(string question)
    {

        var service = new OpenAIService(new OpenAiOptions() { ApiKey = Key });
        var request = new CompletionCreateRequest()
        {
            Prompt = Prompt + question + trail,
            Temperature = 0.3f,
            MaxTokens = 1000
        };

        var response = await service.Completions.CreateCompletion(request, Models.TextDavinciV3);

        if (response.Successful)
        {
            return response.Choices.FirstOrDefault()?.Text ?? string.Empty;
        }
        else
        {
            MessageBox.Show(response.Error?.ToString());
        }

        return string.Empty;
    }
}
