﻿

Public Class frmSalesReportInfo
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Try

            ' declare variables
            Dim strEmailToAddress As String

            ' reset control colors
            cboTimePeriod.BackColor = Color.White
            txtEmail.BackColor = Color.White

            ' declare variables
            Dim strTimePeriod As String

            ' get time period in which to run sales report
            If cboTimePeriod.SelectedItem = "" Then
                cboTimePeriod.BackColor = Color.Yellow
                MessageBox.Show("Please select the time period for which you want to view sales data")

            Else

                strTimePeriod = cboTimePeriod.SelectedItem

                If txtEmail.Text = "" Or System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then

                    txtEmail.BackColor = Color.Yellow
                    MessageBox.Show("Please enter a valid email address.")

                Else
                    strEmailToAddress = txtEmail.Text

                    ' reset control colors
                    cboTimePeriod.BackColor = Color.White
                    txtEmail.BackColor = Color.White

                    ' create sales report
                    Dim blnResult As Boolean = CreateSalesReport(Me, strTimePeriod, False)

                    ' wait for report to finish saving
                    System.Threading.Thread.Sleep(3000)

                    ' email sales report
                    If blnResult = True Then
                        SendMail(strEmailToAddress, "TeamBeesCapstone@gmail.com", "Sales Report", "Attached is your requested sales report.", "TeamBeesCapstone@gmail.com", "cincystate123", "SalesReport.xlsx", False)
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ' close the form
        Me.Close()

    End Sub

    Private Sub frmSalesReportInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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