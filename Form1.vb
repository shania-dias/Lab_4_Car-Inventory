Option Strict On

' Author:       Shania Dias0
' Date:         07/08/2019
' Description:  An application to keep a list of cars

Public Class frmCarInventory

#Region "Declarations"
    Private carList As New SortedList                                 ' collection of Car objects
    Private currentCarIdentificationNumber As String = String.Empty   ' the current selected car's id
    Private editMode As Boolean = False                               ' a variable to identify if edit mode is on or off
#End Region

#Region "Handlers"

    ' validates user input. If it's valid, a Car object will be either created or updated. User input will be displayed 
    ' in the ListView.

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click

        Dim car As Car  ' declare a Car class

        ' validates the user input
        If IsValidInput() Then

            editMode = True    ' turn the edit mode on

            ' if the selected car identification number has no value, then it's a new item 
            If (currentCarIdentificationNumber.Trim.Length = 0) Then

                ' creates a new car object using the constructor
                car = New Car(cmbMake.Text, tbModel.Text.Trim, cmbYear.Text, Convert.ToDouble(tbPrice.Text.Trim), chkNew.Checked)

                ' adds car to the carList collection
                carList.Add(car.IdentificationNumber, car)

                ' if selected car identification number has a value, then it's an existing item
            Else
                car = CType(carList.Item(Convert.ToInt32(currentCarIdentificationNumber)), Car)  ' get the car from the collection
                car.Make() = cmbMake.Text                                                        ' update the car's make
                car.Model() = tbModel.Text.Trim                                                  ' update the car's model
                car.Year() = cmbYear.Text                                                        ' update the car's year
                car.Price() = Convert.ToDouble(tbPrice.Text.Trim)                                ' update the car's price
                car.ShowMeTheMoney() = chkNew.Checked                                            ' update the car's status
            End If

            lvwCars.Items.Clear()   ' clear the items from a ListView control

            ' loop through the carList collection and populate the ListView
            For Each carEntry As DictionaryEntry In carList

                Dim carItem As New ListViewItem     ' declare a ListViewItem class
                car = CType(carEntry.Value, Car)    ' get the car from the list

                carItem.Checked = car.ShowMeTheMoney()   ' assign the value checked control

                ' assign values to the subitems
                carItem.SubItems.Add(car.IdentificationNumber.ToString())
                carItem.SubItems.Add(car.Make)
                carItem.SubItems.Add(car.Model)
                carItem.SubItems.Add(car.Year)
                carItem.SubItems.Add(car.Price().ToString("c"))

                lvwCars.Items.Add(carItem)  ' add the ListViewItem to the ListView control
            Next

            Reset() ' reset the form

        End If
    End Sub

    ' lvwCars_SelectedIndexChanged - when a row in the ListView control is selected
    ' it will populate the fields For editing

    Private Sub lvwCars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        ' set the BackColor of each row to White
        For Each carItem As ListViewItem In Me.lvwCars.Items
            carItem.BackColor = Color.AliceBlue
        Next

        Const subItemIndex As Integer = 1   ' constant that keeps the index of the subitem that hold the car id

        currentCarIdentificationNumber = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(subItemIndex).Text  ' get the car's id

        Dim car As Car = CType(carList.Item(Convert.ToInt32(currentCarIdentificationNumber)), Car)  ' get the car from the collection using the car's id

        cmbMake.Text = car.Make()                       ' gets the car's make and sets the combo box
        tbModel.Text = car.Model()                      ' gets the car's model and sets the text box
        cmbYear.Text = car.Year()                       ' gets the car's year and sets the combo box
        tbPrice.Text = car.Price().ToString()           ' gets the car's price and sets the text box
        chkNew.Checked = car.ShowMeTheMoney()           ' gets the car's status and sets the check box

        Me.lvwCars.FocusedItem.BackColor = Color.LightBlue  ' sets the BackColor of the focused item to LightBlue
        Me.lvwCars.FocusedItem.Selected = False             ' sets the Selected property of the focused item to False

    End Sub


    ' Reset button that resets the form using the subroutine

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    ' Exit button that exits the form

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' lvwCars_ItemCheck - used to prevent the user from checking the check box in the list view if not in edit mode

    Private Sub lvwCars_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck
        If (editMode = False) Then
            e.NewValue = e.CurrentValue
        End If
    End Sub

    ' frmCarInventory that resets the form if the item in the list view is not focused

    Private Sub frmCarInventory_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        ' if the item in the list view is not focused, reset the form
        If (currentCarIdentificationNumber <> String.Empty) Then
            For Each carItem As ListViewItem In Me.lvwCars.Items
                carItem.BackColor = Color.White
            Next
        End If
    End Sub

#End Region

#Region "Functions/Methods"

    ' IsValidInput - validates the data to ensure the user has entered appropriate values

    Private Function IsValidInput() As Boolean

        Dim returnValue As Boolean = True           ' a function return value
        Dim errorMessage As String = String.Empty   ' holds an error message for the user
        Dim price As Double                         ' holds the price of the car

        ' check if the car make has been selected
        If (cmbMake.SelectedIndex = -1) Then

            ' If not set the error message
            errorMessage += "Please select the car's make" & vbCrLf

        End If

        ' check if the model has been entered
        If (tbModel.Text.Trim.Length = 0) Then

            ' If not set the error message
            errorMessage += "Please enter the car's model" & vbCrLf

        End If

        ' check if the year has been selected
        If (cmbYear.SelectedIndex = -1) Then

            ' If not set the error message
            errorMessage += "Please select the car's year" & vbCrLf

        End If

        ' check if the price has been entered
        If (tbPrice.Text.Trim.Length = 0) Then

            ' If not set the error message
            errorMessage += "Please enter the price of the car" & vbCrLf

        Else

            ' check if user input for the price is a real number
            If (Double.TryParse(tbPrice.Text.Trim, price) = False OrElse price < 0.0) Then

                tbPrice.Clear()   ' clear the text box 

                ' If not set the error message
                errorMessage += "Please enter a real positive number for the car's price"

            End If

        End If

        ' if there are some errors, then
        If (errorMessage <> String.Empty) Then

            returnValue = False   ' set the value of returnValue to False

            lbloutputMessage.Text = "ERROR(s)" & vbCrLf & errorMessage  ' display the error message in the lblError

        End If

        Return returnValue  ' return the boolean value (True - if there no errors; False - if there are some errors)

    End Function


    ' Resets the form to its initial state

    Private Sub Reset()

        cmbMake.SelectedIndex = -1              ' set the index of the cmbMake combo box to -1
        tbModel.Text = String.Empty             ' set the Text of the txtModel text box to an empty string
        cmbYear.SelectedIndex = -1              ' set the index of the cmbYear combo box to -1
        tbPrice.Text = String.Empty             ' set the Text of the txtPrice text box to an empty string
        chkNew.Checked = False                  ' uncheck the chkNew check box
        lbloutputMessage.Text = String.Empty    ' set the Text of the lblError label to an empty string
        cmbMake.Select()                        ' set the Focus to the cmbMale combo box
        editMode = False                        ' turn the edit mode off

        ' set the BackColor of each row of the ListView control to White
        For Each carItem As ListViewItem In Me.lvwCars.Items
            carItem.BackColor = Color.White
        Next

        currentCarIdentificationNumber = String.Empty  ' set the current selected car's id to an empty string
    End Sub

    Private Sub LblPrice_Click(sender As Object, e As EventArgs) Handles lblPrice.Click

    End Sub

    Private Sub FrmCarInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

#End Region

End Class
