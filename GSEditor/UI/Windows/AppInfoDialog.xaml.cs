﻿using GSEditor.Common.Utilities;
using System.Windows;

namespace GSEditor.UI.Windows;

public partial class AppInfoDialog : Window
{
  public static readonly string AppVersions = "" +
    "### v1.2.6\n" +
    "* 항목 검색 기능에서 창이 열릴경우 입력을 바로 할 수 있도록 변경\n" +
    "* 체크섬 기록이 재대로 되지 않는 문제를 수정\n" +
    "* 외부 원인에 의한 깨진 데이터를 알림, 데이터 수정 기능 추가 (이미지, 진화·배우는 기술 데이터만 지원)\n" +
    "* 성별관련 오류 수정\n" +
    "* 이미지 변경 시 일부 도트가 누락되는 버그 수정\n" +
    "* 외부툴에 의한 이미지 손상 로딩 개선\n" +
    "* 오류 알림 개선\n" +
    "  \n" +
    "  \n" +
    "### v1.2.5\n" +
    "* 메뉴 순서 변경 (포켓몬, 아이템, 안농, 기술, 기술머신 > 포켓몬, 안농, 아이템, 기술, 기술머신)\n" +
    "* 콤보상자 홀수, 짝수 구분되도록 스타일 변경\n" +
    "* 콤보상자에서 오른쪽 클릭을 이용하여 항목을 찾을 수 있도록 변경\n" +
    "  \n" +
    "  \n" +
    "### v1.2.4\n" +
    "* 기술머신 메뉴 포켓몬 상세 설정 추가\n" +
    "* 설명란의 길이가 처음에 표시안되는 문제 수정\n" +
    "* 비전, 기술머신 목록 홀수, 짝수 구분되도록 변경 및 버그 수정\n" +
    "  \n" +
    "  \n" +
    "### v1.2.3\n" +
    "* 기술머신 메뉴 추가\n" +
    "  \n" +
    "  \n" +
    "### v1.2.2\n" +
    "* 이미지 png, 2bpp 내보내기 시 기본 파일명 추가\n" +
    "* 목록 상자 검색 기능 표시 버그 수정\n" +
    "* 포켓몬 포획률 퍼센티지(%) 표시\n" +
    "* 안농 메뉴 추가\n" +
    "  \n" +
    "  \n" +
    "### v1.2.1\n" +
    "* 포켓몬 성장률 항목 수정\n" +
    "* 기술 메뉴 항목 수정 (애니메이션 값 > 기술 번호)\n" +
    "  \n" +
    "  \n" +
    "### v1.2.0\n" +
    "* UI 개선 및 재설계\n" +
    "* hidpi+ 지원 (배율이 지정되어 있는 높은 해상도의 모니터에서 지원)\n" +
    "* 이제부터 은 버전 편집을 미지원합니다. (오류가 발생할 수 있음)\n" +
    "* 이제부터 성도맵 호환성 지원하지 않습니다. (포켓몬 이름 관련 사항으로 성도맵 툴을 이용시 불편할 수 있음)\n" +
    "* 이제부터 롬 파일 헤더에 관하여 알림을 표시하지 않습니다.\n" +
    "* 포켓몬 이미지, 팔레트 편집 및 추출 추가\n" +
    "* 롬 파일 헤더 체크섬이 기록되도록 업데이트\n" +
    "* 기술 편집 탭 추가\n" +
    "* 목록 검색 기능 추가\n" +
    "  \n" +
    "  \n" +
    "### v1.1.4\n" +
    "* 저장, 텍스트 UI 수정\n" +
    "* 성도맵 호환에 따른 예외처리 추가\n" +
    "* 에뮬레이터 미설정 시 나오는창에서 선택사항 묻기 추가\n" +
    "  \n" +
    "  \n" +
    "### v1.1.0\n" +
    "* 진화, 자력기 편집 버튼 UI 수정\n" +
    "* 자력기 편집 중 가져오기 기능 추가\n" +
    "  \n" +
    "  \n" +
    "### v1.0.5\n" +
    "* 롬을 여러번 불러와도 정상 작동하도록 수정\n" +
    "* 기타 UI 위치 조절\n" +
    "  \n" +
    "  \n" +
    "### v1.0.4\n" +
    "* 롬 파일 헤더 검사를 자세히 하도록 수정\n" +
    "* 은 버전에서 아이템 수정이 불가능한 부분 수정\n" +
    "* 포켓몬 선택 유지 체크항목 기억하도록 수정\n" +
    "  \n" +
    "  \n" +
    "### v1.0.3\n" +
    "* 롬을 열 때 헤더를 검사하도록 수정 (비정상인 경우 경고창 팝업)\n" +
    "* 능력치 입력 순서 변경\n" +
    "* 소스 리팩터링\n" +
    "  \n" +
    "  \n" +
    "### v1.0.2\n" +
    "* 데이터 저장이 재대로 안되는 현상 수정\n" +
    "* 롬을 여러번 불러오지 못하도록 변경\n" +
    "  \n" +
    "  \n" +
    "";

  public AppInfoDialog()
  {
    InitializeComponent();

    VersionLabel.Content = $"버전：{Platforms.AppVersion}";
    ContentMarkdown.HereMarkdown = AppVersions;
  }
}
