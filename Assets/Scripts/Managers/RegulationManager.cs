using System.Collections;
using System.Collections.Generic;

public class RegulationManager
{
    private List<Regulation> regulations;

    public RegulationManager()
    {
        if (GameManager.Instance.savedRegulations.Count == 0)
        {
            regulations = CreateInitialRegulations();
            GameManager.Instance.savedRegulations = new List<Regulation>(regulations);
        }
        else
        {
            regulations = new List<Regulation>(GameManager.Instance.savedRegulations);
        }
    }

    private List<Regulation> CreateInitialRegulations()
    {
        return new List<Regulation>
        {
            // 0, 1
            CreateRegulation("", new List<string> { "" }), // 표지
            CreateRegulation("", new List<string> { "" }), // 표지

            // 2, 3
            CreateRegulation("Welcome", new List<string>
            {
                "당신의 하루는 사무실에서부터 시작합니다.",
                "",
                "동료들과 이야기를 나누며 업무를 시작하십시오.",
                "동료들과의 소통 또한 당신의 업무에 해당합니다."
            }),
            CreateRegulation("플레이 방법", new List<string>
            {
                "테스트 버전 기준 플레이 방법입니다.",
                "",
                "'다음' 버튼을 눌러 고객의 서류를 받습니다.",
                "서브 데스크에 있는 서류들을 메인 데스크로 옮길 수 있습니다.",
                "",
                "'비교' 기능을 사용하여 서류의 오류를 확인할 수 있습니다.",
                "서류의 내용을 검토하여 오류가 있는지 확인하십시오."
            }),

            // 4, 5
            CreateRegulation("", new List<string>
            {
                "메인 데스크 하단에 있는 손잡이를 눌러 도장을 꺼낼 수 있습니다.",
                "송장 하단에 고객의 택배를 받을 것인지 거부할 것인지 도장을 찍으십시오.",
                "도장을 찍은 후 송장 하단의 절단선을 따라 오려내어 결과를 고객에게 전달하십시오.",
                "",
                "남은 송장의 좌상단 빨간 박스를 누르면 부착이 가능합니다.",
                "스티커 상태가 된 송장을 택배에 붙이십시오.",
                "",
                ""
            }),
            CreateRegulation("", new List<string>
            {
                "허가된 택배물은 송장을 붙인 뒤 메인 데스크 상단으로 제출하십시오.",
                "남은 서류들은 고객에게 전달하십시오."
            }),

            // 6, 7
            CreateRegulation("기본 규칙", new List<string>
            {
                "신청인은 신분증을 제출해야 함",
                "모든 문서는 최신화되어야 함",
                "택배는 보내는 이가 직접 접수해야 함"
            }, new List<TextObjectType>
            {
                TextObjectType.Regulation,
                TextObjectType.Today,
                TextObjectType.TrackingNumber,
                TextObjectType.Name
            }),
            CreateRegulation("", new List<string> { "" }),

            // 8, 9
            //CreateRegulation("TestPage", new List<string>
            //{
            //    "한 페이지에 8개의 텍스트만 들어갑니다.",
            //    "",
            //    "옆의 페이지에는 10개의 텍스트가 생성되게 했지만",
            //    "8개만 생성된 것을 확인할 수 있습니다."
            //}),
            //CreateRegulation("TestPage", new List<string>
            //{
            //    "1신청인은 신분증을 제출해야 함",
            //    "2모든 문서는 최신화되어야 함",
            //    "3유효기간이 지난 송장은 사용할 수 없음",
            //    "4신청인은 신분증을 제출해야 함",
            //    "5모든 문서는 최신화되어야 함",
            //    "6유효기간이 지난 송장은 사용할 수 없음",
            //    "7신청인은 신분증을 제출해야 함",
            //    "8모든 문서는 최신화되어야 함",
            //    "9유효기간이 지난 송장은 사용할 수 없음",
            //    "10택배는 보내는 이가 직접 접수해야 함"
            //}),

            //// 10, 11
            //CreateRegulation("추가 조건1", new List<string> { "추가 내용" }),
            //CreateRegulation("추가 조건2", new List<string> { "추가 내용" }),

            //// 12, 13
            //CreateRegulation("추가 조건3", new List<string> { "추가 내용" }),
            //CreateRegulation("추가 조건4", new List<string> { "추가 내용" })
        };
    }

    private Regulation CreateRegulation(string title, List<string> texts, List<TextObjectType> types = null)
    {
        if (types == null)
        {
            types = new List<TextObjectType>();
            for (int i = 0; i < texts.Count; i++)
            {
                types.Add(TextObjectType.Regulation); // 기본값으로 TextObjectType.Regulation 설정
            }
        }

        List<RegulationText> regulationTexts = new List<RegulationText>();
        for (int i = 0; i < texts.Count; i++)
        {
            regulationTexts.Add(new RegulationText(texts[i], types[i]));
        }

        return new Regulation(title, regulationTexts);
    }

    public List<Regulation> GetRegulations()
    {
        return regulations;
    }

    public void AddRegulation(Regulation newRegulation)
    {
        regulations.Add(newRegulation);
        SaveRegulations();
    }

    public void InsertRegulation(int index, Regulation newRegulation)
    {
        regulations.Insert(index, newRegulation);
        SaveRegulations();
    }

    public void UpdateRegulation(int index, string title, List<RegulationText> texts)
    {
        if (index >= 0 && index < regulations.Count)
        {
            regulations[index] = new Regulation(title, texts);
            SaveRegulations();
        }
    }

    public void RemoveRegulation(int index)
    {
        if (index >= 0 && index < regulations.Count)
        {
            regulations.RemoveAt(index);
            SaveRegulations();
        }
    }

    private void SaveRegulations()
    {
        GameManager.Instance.savedRegulations = new List<Regulation>(regulations);
    }
}
