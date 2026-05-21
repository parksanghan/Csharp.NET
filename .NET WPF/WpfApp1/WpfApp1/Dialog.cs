/// @file		Dialog.cs
/// @brief		대화상자 오픈 컨트롤러 파일
/// @author		Jongwon Seo (jwseo@soletop.com)
/// @date		2018-04-10
/// @remark		공통
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

 
    /// @class	Dialog
    /// @brief	대화상자 오픈 컨트롤러
    /// @author	Jongwon Seo (jwseo@soletop.com)
    /// @date	2015-09-04
    /// @details 시나리오에 고장을 추가하는 대화상자 클래스이다.
    /// @remark	공통
    public class Dialog
    {

        /// @brief	대화상자 오픈 함수
        /// @author	Jongwon Seo (jwseo@soletop.com)
        /// @date	2015-09-04
        /// @param	Owner : 대화상자 부모
        /// @param	dialog : 목표 대화상자
        /// @param	IsActivateOnly : 대화상자만 컨트롤할지 여부
        /// @return	none
        /// @details 대화상자 오픈함수이다.
        public static void ShowDialog(Window Owner, Window dialog, bool IsActivateOnly = true)
        {
            /// <para/>1. 목표 대화상자의 부모를 Owner로 할당
            dialog.Owner = Owner;

            /// <para/>2. Owner가 null이라면 대화상자의 부모는 메인윈도우로
            if (Owner == null)
                dialog.Owner = Application.Current.MainWindow;

            /// <para/>3. IsActivateOnly이 참이라면 현재 대화상자만 컨트롤 할 수 있도록 하고 거짓이라면 대화상자 및 다른 윈도우도 컨트롤 할 수 있다.
            if (IsActivateOnly)
                dialog.ShowDialog();
            else
                dialog.Show();
        }


        /// @brief	생성자 함수
        /// @author	Jongwon Seo (jwseo@soletop.com)
        /// @date	2015-09-04
        /// @param	Owner : 대화상자 부모
        /// @param	dialog : 목표 대화상자
        /// @return	대화상자의 결과값 반환
        /// @details 대화상자 오픈함수이다.
        public static bool ShowDialog(Window Owner, Window dialog)
        {
            /// <para/>1. Owner가 null이라면 대화상자의 부모는 메인윈도우로
            if (Owner == null)
                dialog.Owner = Application.Current.MainWindow;
            else
                // 목표 대화상자의 부모를 Owner로 할당
                dialog.Owner = Owner;

            /// <para/>2. 대화상자의 결과 반환
            return dialog.ShowDialog().Value;
        }
    }

/// @class	DialogExtention
/// @brief	대화상자 확장메소드 클래스
/// @author	Jongwon Seo (jwseo@soletop.com)
/// @date	2022-09-21
/// @details 대화상자에서 스크린샷을 찍기위한 클래스이다.
/// @remark	공통
public static class DialogExtention
{

    /// @brief		키 이벤트 등록 및 해제
    /// @author		Jongwon Seo (jwseo@soletop.com)
    /// @date		2022-09-21
    /// @param		window : Window객체
    /// @return		none
    /// @details	스크린샷 키 이벤트를 처리하기 위해 키 이벤트를 등록하는 함수이다.
    public static void SetKeyEvent(this Window window)
    {
        /// <para/>1. 키 이벤트 해제 및 등록
        window.KeyUp -= Window_KeyUp;
        window.KeyUp += Window_KeyUp;
    }


    /// @brief		키 이벤트 호출
    /// @author		Jongwon Seo (jwseo@soletop.com)
    /// @date		2022-09-21
    /// @param		sender : 이벤트 호출 객체
    /// @param		e : 이벤트 매개변수
    /// @return		none
    /// @details	키보드를 뗐을 때 이벤트가 호출되는 함수이다.
    private static void Window_KeyUp(object sender, KeyEventArgs e)
    {
        /// <para/>1. 전달된 파라미터의 키가 "P"라면 스크린샷 클래스의 Shot()를 호출한다.
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.P)
        {
             
        }
    }



    /// @brief		키 이벤트 해제
    /// @author		Jongwon Seo (jwseo@soletop.com)
    /// @date		2022-09-21
    /// @param		window : Window객체
    /// @return		none
    /// @details	대화상자가 닫힐 때 호출되는 함수이다.
    public static void ReleaseKeyEvent(this Window window)
    {
        /// <para/>1. 키 이벤트 해제
        window.KeyUp -= Window_KeyUp;
    }
}