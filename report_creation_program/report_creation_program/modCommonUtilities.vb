﻿Imports Excel = Microsoft.Office.Interop.Excel

Module modCommonUtilities

    ' Open a form and hide the current form
    Public Function OpenFormMaintainParent(ByRef frmSelf As Form, ByVal frmToOpen As Form) As DialogResult

        ' Init variables
        Dim dlgResult As DialogResult

        ' Make self invisible
        frmSelf.Visible = False

        ' Make new form
        dlgResult = frmToOpen.ShowDialog()

        ' Make self visible
        If (frmSelf.IsDisposed = False) Then
            frmSelf.Visible = True
        End If

        ' Return result
        Return dlgResult

    End Function

    ' Open a form and close the current form
    Public Function OpenFormKillParent(ByRef frmSelf As Form, ByVal frmToOpen As Form) As DialogResult

        ' Init variables
        Dim dlgResult As DialogResult

        ' Make new form
        frmToOpen.Show()

        ' Kill self
        frmSelf.Close()

        ' Return result
        Return dlgResult

    End Function

    Public Sub ButtonColor(ByVal pntPosition As Point, ByRef btnItemLookup As Button, ByVal frmMe As Form, ByVal btmButtonGray As Bitmap, ByVal btmButton As Bitmap, Optional ByVal intUpperOffset As Integer = -9, Optional ByVal intLowerOffset As Integer = -8)

        If (MouseIsHovering(pntPosition, btnItemLookup, frmMe, intUpperOffset, intLowerOffset) And frmMe.ContainsFocus = True) Then
            btnItemLookup.Image = btmButtonGray
        Else
            btnItemLookup.Image = btmButton
        End If

    End Sub

    Public Function MouseIsHovering(ByVal MousePosition As Point, ByRef ctlControl As Control, ByRef frmForm As Form, Optional ByVal intUpperOffset As Integer = -9, Optional ByVal intLowerOffset As Integer = -8)

        ' This if statement adapted from the Waveslash game launch I made for my latest game: https://gravityhamster.itch.io/waveslash
        If (MousePosition.X > ctlControl.Left + frmForm.Left And MousePosition.X < ctlControl.Right + frmForm.Left) And (MousePosition.Y > ctlControl.Top + frmForm.Top + ctlControl.Height + intUpperOffset And MousePosition.Y < ctlControl.Bottom + frmForm.Top + ctlControl.Height + intLowerOffset) Then
            Return True
        Else
            Return False
        End If

    End Function

    ' Clamp a value between two values
    Public Function Clamp(ByVal intValue As Integer, ByVal intMin As Integer, ByVal intMax As Integer) As Integer

        ' Clamp into range
        If (intValue > intMax) Then
            intValue = intMax
        ElseIf (intValue < intMin) Then
            intValue = intMin
        End If

        ' Return
        Return intValue

    End Function

    Public Sub CreateSalesReport(ByRef frmMe As Form, ByVal strTimePeriod As String, ByVal blnQuiet As Boolean)

        ' instantiate excel objects and declare variables
        ' THE EXCEL CODE IS BASED ON THIS TUTORIAL: https://www.tutorialspoint.com/vb.net/vb.net_excel_sheet.htm
        Dim ExcelApp As Excel.Application
        Dim ExcelWkBk As Excel.Workbook
        Dim ExcelWkSht As Excel.Worksheet
        Dim ExcelRange As Excel.Range

        ' start excel and get application object
        ExcelApp = CreateObject("Excel.Application")
        ExcelApp.Visible = False ' for testing only, set to false when go to prod

        ' Add a new workbook
        ExcelWkBk = ExcelApp.Workbooks.Add
        ExcelWkSht = ExcelWkBk.ActiveSheet

        ' add table headers going cell by cell
        ExcelWkSht.Cells(1, 1) = "Sales for the " & strTimePeriod
        ExcelWkSht.Cells(2, 1) = "Medals"
        ExcelWkSht.Cells(2, 2) = "Statues"
        ExcelWkSht.Cells(2, 3) = "Books"
        ExcelWkSht.Cells(2, 4) = "Church Goods"
        ExcelWkSht.Cells(2, 5) = "Tokens"
        ExcelWkSht.Cells(2, 6) = "Baptism"
        ExcelWkSht.Cells(2, 7) = "Rosary"
        ExcelWkSht.Cells(2, 8) = "Wedding"
        ExcelWkSht.Cells(2, 9) = "Anniversary"
        ExcelWkSht.Cells(2, 10) = "Cards"
        ExcelWkSht.Cells(2, 11) = "Holy Cards"
        ExcelWkSht.Cells(2, 12) = "Pictures/Artwork"
        ExcelWkSht.Cells(2, 13) = "Confirmation"
        ExcelWkSht.Cells(2, 14) = "First Communion"

        ' add table data
        ExcelWkSht.Cells(3, 1) = GetSales(frmMe, 1, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 2) = GetSales(frmMe, 2, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 3) = GetSales(frmMe, 3, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 4) = GetSales(frmMe, 4, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 5) = GetSales(frmMe, 5, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 6) = GetSales(frmMe, 6, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 7) = GetSales(frmMe, 7, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 8) = GetSales(frmMe, 8, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 9) = GetSales(frmMe, 9, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 10) = GetSales(frmMe, 10, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 11) = GetSales(frmMe, 11, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 12) = GetSales(frmMe, 12, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 13) = GetSales(frmMe, 13, strTimePeriod, blnQuiet)
        ExcelWkSht.Cells(3, 14) = GetSales(frmMe, 14, strTimePeriod, blnQuiet)

        Dim strFile As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\SalesReport.xlsx"

        ' Save
        If (My.Computer.FileSystem.FileExists(strFile) = True) Then
            My.Computer.FileSystem.DeleteFile(strFile)
        End If
        ExcelWkSht.SaveAs(strFile)

        ' Release object references.
        ExcelRange = Nothing
        ExcelWkSht = Nothing
        ExcelWkBk = Nothing
        ExcelApp.Quit()
        ExcelApp = Nothing
        Exit Sub
Err_Handler:
        If (blnQuiet = False) Then
            MsgBox(Err.Description, vbCritical, "Error: " & Err.Number)
        Else
            Console.WriteLine(Err.Description)
        End If

    End Sub

    Public Function GetSales(ByRef frmMe As Form, ByVal intCategory As Integer, ByVal strTimePeriod As String, ByVal blnQuiet As Boolean)

        Dim dblTotalSales As Double

        Try

            Dim strSelect As String
            Dim cmdSelect As OleDb.OleDbCommand
            Dim dt As DataTable = New DataTable

            ' Open the DB
            If OpenDatabaseConnectionSQLServer() = False Then

                ' The database is not open
                If blnQuiet = False Then
                    MessageBox.Show(frmMe, "Database connection error." & vbNewLine &
                                "The form will now close.",
                                frmMe.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Close the form/application
                    frmMe.Close()
                Else
                    Console.WriteLine("Database connection error." & vbNewLine & "Report not generated.")
                End If

            End If

                ' Build the select statement based on user-selected time period
                If strTimePeriod = "last day" Then
                strSelect = "SELECT SUM(decItemPrice) from ItemsSoldByCategoryWithPriceAndDate where intCategoryID = " & intCategory & " AND strPurchaseDate > (DATEADD(DAY, -1, GETDATE()))"

            ElseIf strTimePeriod = "last week" Then
                strSelect = "Select SUM(decItemPrice) from ItemsSoldByCategoryWithPriceAndDate where intCategoryID = " & intCategory & " and strpurchasedate > (DATEADD(DAY, -7, GETDATE()))"

            ElseIf strTimePeriod = "last month (30 days)" Then
                strSelect = "SELECT SUM(decItemPrice) from ItemsSoldByCategoryWithPriceAndDate where intCategoryID = " & intCategory & " and strpurchasedate > (DATEADD(DAY, -30, GETDATE()))"

            ElseIf strTimePeriod = "last year (365 days)" Then
                strSelect = "SELECT SUM(decItemPrice) from ItemsSoldByCategoryWithPriceAndDate where intCategoryID = " & intCategory & " and strpurchasedate > (DATEADD(DAY, -365, GETDATE()))"

            End If

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            Dim objTotalSales As Object = cmdSelect.ExecuteScalar

            ' check for null entries (zeroes), set to zero
            If IsDBNull(objTotalSales) Then
                dblTotalSales = 0
            Else
                dblTotalSales = CDbl(objTotalSales)
            End If

            ' close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception

            If blnQuiet = False Then
                ' Log and display error message
                MessageBox.Show(excError.Message)
            Else
                ' Log message in console
                Console.WriteLine(excError.Message)
            End If

        End Try

        Return dblTotalSales

    End Function

    Public Sub ReadCSVFile()

        Try
            Dim intY As Integer
            Dim intReader As Integer = 0
            Dim strFile As String = My.Computer.FileSystem.ReadAllText("ReportFlags.csv")

            ' Reset CSV array
            aastrCSVFile = {
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""},
                {"", ""}
            }

            For intY = 0 To 15

                ' Read until comma
                While (strFile.Chars(intReader) <> ",")
                    aastrCSVFile(intY, 0) += strFile.Chars(intReader)
                    intReader += 1
                End While
                intReader += 1

                ' Read until NewLine
                While (strFile.Chars(intReader) <> vbCr)
                    aastrCSVFile(intY, 1) += strFile.Chars(intReader)
                    intReader += 1
                End While
                intReader += 2
            Next

        Catch excError As Exception

            Console.WriteLine("ERROR: Could not read CSV file. " & excError.Message)

            ' Attempt to delete the CSV file
            Try

                My.Computer.FileSystem.DeleteFile("ReportFlags.csv")

                ' Re-write
                If (My.Computer.FileSystem.FileExists("ReportFlags.csv") = False) Then

                    ' Reset the local file
                    aastrCSVFile = {
                        {"Sales Report Daily", "false"},
                        {"Sales Report Weekly", "false"},
                        {"Sales Report Monthly", "false"},
                        {"Sales Report Yearly", "false"},
                        {"Sales Tax Report Daily", "false"},
                        {"Sales Tax Report Weekly", "false"},
                        {"Sales Tax Report Monthly", "false"},
                        {"Sales Tax Report Yearly", "false"},
                        {"Sales Inventory Daily", "false"},
                        {"Sales Inventory Weekly", "false"},
                        {"Sales Inventory Monthly", "false"},
                        {"Sales Inventory Yearly", "false"},
                        {"Sales Deposit Daily", "false"},
                        {"Sales Deposit Weekly", "false"},
                        {"Sales Deposit Monthly", "false"},
                        {"Sales Deposit Yearly", "false"}
                    }

                    WriteCSVFile()

                    ' Re-read
                    If (My.Computer.FileSystem.FileExists("ReportFlags.csv") = True) Then

                        ReadCSVFile()

                    End If

                End If

            Catch excNestError As Exception

                Console.WriteLine("ERROR: Could not delete CSV file. " & excError.Message)

            End Try

        End Try



    End Sub

    Public Sub WriteCSVFile()

        Try

            Dim strWriteLine As String =
            aastrCSVFile(0, 0) & "," & aastrCSVFile(0, 1) & vbNewLine &
            aastrCSVFile(1, 0) & "," & aastrCSVFile(1, 1) & vbNewLine &
            aastrCSVFile(2, 0) & "," & aastrCSVFile(2, 1) & vbNewLine &
            aastrCSVFile(3, 0) & "," & aastrCSVFile(3, 1) & vbNewLine &
            aastrCSVFile(4, 0) & "," & aastrCSVFile(4, 1) & vbNewLine &
            aastrCSVFile(5, 0) & "," & aastrCSVFile(5, 1) & vbNewLine &
            aastrCSVFile(6, 0) & "," & aastrCSVFile(6, 1) & vbNewLine &
            aastrCSVFile(7, 0) & "," & aastrCSVFile(7, 1) & vbNewLine &
            aastrCSVFile(8, 0) & "," & aastrCSVFile(8, 1) & vbNewLine &
            aastrCSVFile(9, 0) & "," & aastrCSVFile(9, 1) & vbNewLine &
            aastrCSVFile(10, 0) & "," & aastrCSVFile(10, 1) & vbNewLine &
            aastrCSVFile(11, 0) & "," & aastrCSVFile(11, 1) & vbNewLine &
            aastrCSVFile(12, 0) & "," & aastrCSVFile(12, 1) & vbNewLine &
            aastrCSVFile(13, 0) & "," & aastrCSVFile(13, 1) & vbNewLine &
            aastrCSVFile(14, 0) & "," & aastrCSVFile(14, 1) & vbNewLine &
            aastrCSVFile(15, 0) & "," & aastrCSVFile(15, 1) & vbNewLine

            My.Computer.FileSystem.WriteAllText("ReportFlags.csv", strWriteLine, False)

        Catch excError As Exception
            Console.WriteLine("ERROR: Could not write CSV file. " & excError.Message)
        End Try



    End Sub

End Module
