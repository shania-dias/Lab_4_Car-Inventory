Option Strict On
Public Class Car

    Private Shared carCount As Integer = 0          ' shared private variable to hold the number of cars
    Private carIdentificationNumber As Integer = 0  ' private variable to hold the car's identification number
    Private carMake As String = String.Empty        ' private variable to hold the car's make
    Private carModel As String = String.Empty       ' private variable to hold the car's model
    Private carYear As String = String.Empty        ' private variable to hold the car's year
    Private carPrice As Double = 0.0                ' private variable to hold the car's price
    Private carShowMeTheMoney As Boolean = False    ' private variable to hold the car's status

#Region "Constructors"

    ' Default constructor - creates a new car object

    Public Sub New()
        carCount += 1                        ' increment the shared variable counter by 1
        carIdentificationNumber = carCount   ' assign the car's id
    End Sub

    ' Parameterized constructor - creates a new car object
    Public Sub New(make As String, model As String, year As String, price As Double, status As Boolean)
        Me.New()                         ' calls the default constructor
        carMake = make                   ' sets the car's make
        carModel = model                 ' sets the car's model
        carYear = year                   ' sets the car's year
        carPrice = Math.Round(price, 2)  ' sets the car's pricw
        carShowMeTheMoney = status       ' sets the car's status
    End Sub
#End Region

#Region "Properties"

    ' Count ReadOnly Property - gets the number of cars that have been instantiated

    Public ReadOnly Property Count() As Integer
        Get
            Return carCount
        End Get
    End Property

    ' IdentificationNumber ReadOnly Property - gets a car's id

    Public ReadOnly Property IdentificationNumber() As Integer
        Get
            Return carIdentificationNumber
        End Get
    End Property

    ' gets and sets a car's make

    Public Property Make() As String
        Get
            Return carMake
        End Get
        Set(value As String)
            carMake = value
        End Set
    End Property


    ' gets and sets a car's model

    Public Property Model() As String
        Get
            Return carModel
        End Get
        Set(value As String)
            carModel = value
        End Set
    End Property


    ' gets and sets a car's year

    Public Property Year() As String
        Get
            Return carYear
        End Get
        Set(value As String)
            carYear = value
        End Set
    End Property

    ' gets and sets a car's price

    Public Property Price() As Double
        Get
            Return carPrice
        End Get
        Set(value As Double)
            carPrice = Math.Round(value, 2)
        End Set
    End Property

    ' gets and sets a car's status

    Public Property ShowMeTheMoney() As Boolean
        Get
            Return carShowMeTheMoney
        End Get
        Set(value As Boolean)
            carShowMeTheMoney = value
        End Set
    End Property
#End Region

#Region "Methods"
    '
    ' GetCarData is a function that returns information about a car

    Public Function GetCarData() As String
        Return "The car's ID is " & carIdentificationNumber.ToString() & ". The car's make is " & carMake & ". The car's model is " & carModel & ". The car's year is " & carYear & ". The car's price is " & carPrice.ToString("c") & ". The car is " & If(carShowMeTheMoney = True, "new.", "used.").ToString()
    End Function
#End Region

End Class
