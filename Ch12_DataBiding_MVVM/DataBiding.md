# DataBinding(데이터바인딩) : MVVM

## MVVM 개요
최종적으로 완성할 구조는 MVVM(Model-View-ViewModel)패턴의 WPF 앱이다.  
MVVM 패턴은 WPF앱의 표준 설계 방식이며, 세 가지 역할로 코드를 분리한다.  

역할 | 담당 | 예시
---|---|---
Model | 데이터와 비즈니스 로직 | Character 클래스, 데이터 저장/계산
View | 화면(XAML), 사용자UI | TextBox, ListBox, Button 등 UI
ViewModel | View와 Model 사이의 중재자 (프레젠테이션로직) | View에 표시할 데이터를 가공, 사용자 입력을 Model에 전달

> 비즈니스로직과 프레젠테이션 로직을 구분하는 팁은 화면의 유무로 판단하면 좋다.
화면이 없어도 프로그램에 필요한 로직이면 비즈니스 로직, 화면이 없으면 없어도 될 로직은 프레젠티이션 로직

현재 까지는 View와 데이터 로직이 코드 비하인드 한 파일에 섞여 있다.
MVVM은 이것을 분리하여, View는 화면만, ViewModel은 로직만 담장하게 한다.
이 분리를 가능하게 하는 핵심 기술이 DataBinding이다.