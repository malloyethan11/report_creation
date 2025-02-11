﻿Public Class frmCashCreditDepositReportInfo
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Try
            ' reset control colors
            txtYear.BackColor = Color.White
        txtEmail.BackColor = Color.White

        ' declare variables
        Dim strToEmail As String
        Dim strYear As String
        Dim strMonth As String
        Dim strDay As String
        Dim strFile As String

            ' validate input
            If txtEmail.Text = "" Or System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then
                txtEmail.BackColor = Color.Yellow
                MessageBox.Show("Please enter a valid email address.")

            Else
                strToEmail = txtEmail.Text

                If txtYear.Text = "" Or txtYear.Text.Length <> 4 Or IsNumeric(txtYear.Text) = False Then
                    txtYear.BackColor = Color.Yellow
                    MessageBox.Show("Please enter a numeric year in the format YYYY.")

                Else

                    strYear = txtYear.Text

                    If ValidateMonth() = True Then

                        strMonth = txtMonth.Text

                        If ValidateDay() = True Then

                            txtYear.BackColor = Color.White
                            txtEmail.BackColor = Color.White

                            strDay = txtDay.Text
                            Dim blnResult As Boolean = RunCashCreditReport(Me, False, strYear, strMonth, strDay)

                            'GC.Collect()
                            'GC.WaitForPendingFinalizers()

                            'strFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\CashCreditDepositReport.xlsx"

                            'strFile = strFile.Remove(0, 6)

                            Threading.Thread.Sleep(3000)

                            If blnResult = True Then
                                SendMail(strToEmail, "TeamBeesCapstone@gmail.com", "Cash/Credit Deposit Report", "", "TeamBeesCapstone@gmail.com", "cincystate123", "CashCreditDepositReport.xlsx", False)
                            End If
                            'strFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\CashCreditDepositReport.xlsx"


                        End If
                    End If
                End If
            End If

        Catch excError As Exception

            MessageBox.Show("Something went wrong in creating that report." & vbNewLine & "Error: " & excError.Message, "Error!")

        Finally

            ' In case garbage wasn't collected
            GC.Collect()
            GC.WaitForPendingFinalizers()

        End Try

    End Sub

    Private Function ValidateMonth() As Boolean

        Dim blnValidate As Boolean = True

        txtMonth.BackColor = Color.White

        If txtMonth.Text = "" Or txtMonth.Text.Length <> 2 Or IsNumeric(txtMonth.Text) = False Then
            txtMonth.BackColor = Color.Yellow
            MessageBox.Show("Please enter a valid numeric month in the format MM.")
            blnValidate = False
        ElseIf IsNumeric(txtMonth.Text) = True Then
            If CInt(txtMonth.Text) < 1 Or CInt(txtMonth.Text) > 12 Then
                txtMonth.BackColor = Color.Yellow
                MessageBox.Show("Please enter a valid numeric month in the format MM.")
                blnValidate = False
            End If
        End If

        Return blnValidate

    End Function

    Private Function ValidateDay() As Boolean

        Dim blnValidate As Boolean = True
        Dim IntDaysInMonth As Integer = DateTime.DaysInMonth(CInt(txtYear.Text), CInt(txtMonth.Text))

        txtDay.BackColor = Color.White

        If txtDay.Text = "" Or txtDay.Text.Length <> 2 Or IsNumeric(txtDay.Text) = False Then
            txtDay.BackColor = Color.Yellow
            MessageBox.Show("Please enter a valid numeric day in the format DD.")
            blnValidate = False
        ElseIf IsNumeric(txtDay.Text) = True Then
            If CInt(txtDay.Text) > IntDaysInMonth Or CInt(txtDay.Text) < 1 Then
                txtDay.BackColor = Color.Yellow
                MessageBox.Show("Please enter a valid numeric day in the format DD.")
                blnValidate = False
            End If
        End If

        Return blnValidate

    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ' close the form
        Me.Close()

    End Sub

    Private Sub frmCashCreditDepositReportInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.CenterToScreen()

        For Each Control In Controls
            If Control.GetType() = GetType(Button) Then
                Control.FlatStyle = FlatStyle.Flat
                Control.ForeColor = BackColor
                Control.FlatAppearance.BorderColor = BackColor
                Control.FlatAppearance.MouseOverBackColor = BackColor
                Control.FlatAppearance.MouseDownBackColor = BackColor
            End If
        Next

    End Sub

    Private Sub tmrUpdateButtonImage_Tick(sender As Object, e As EventArgs) Handles tmrUpdateButtonImage.Tick

        For Each Control In Controls
            If Control.GetType() = GetType(Button) Then
                ButtonColor(MousePosition, Control, Me, btmButtonShortGray, btmButtonShort)
            End If
        Next

    End Sub
End Class