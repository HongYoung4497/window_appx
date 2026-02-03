# Window AppX Calculator

간단한 사칙연산 계산기 UWP 앱입니다. Windows에서 AppX 패키지로 빌드할 수 있도록 구성했습니다.

## 기능
- 숫자 입력, 소수점
- +, −, ×, ÷ 연산
- 퍼센트, 부호 전환, 전체 초기화

## AppX 빌드 방법 (Windows)
1. Visual Studio에서 `CalculatorApp/CalculatorApp.csproj`를 엽니다.
2. `Release` + `x64` 구성으로 선택합니다.
3. **빌드 > 솔루션 빌드**를 실행하면 `AppPackages` 폴더에 `.appx` 패키지가 생성됩니다.
   - `AppxPackageSigningEnabled`는 `false`로 설정되어 있으니, 배포 시 서명이 필요하면 Visual Studio에서 서명 옵션을 켜세요.

## 실행
- 빌드 후 생성된 `.appx` 파일을 더블 클릭해 설치할 수 있습니다.
