Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq

Public Class BKTree
    Private mRoot As node

    Public Sub Add(ByVal argWord As String)
        argWord = argWord.ToLower()

        If (mRoot Is Nothing) Then
            mRoot = New Node(argWord)
            Exit Sub
        End If

        Dim lcCurrentNode = mRoot

        Dim lcDistance = LevenshteinDistance(lcCurrentNode.Word, argWord)

        While (lcCurrentNode.ContainsKey(lcDistance))
            If (lcDistance = 0) Then Exit Sub

            lcCurrentNode = lcCurrentNode(lcDistance)
            lcDistance = LevenshteinDistance(lcCurrentNode.Word, argWord)

        End While

        lcCurrentNode.AddChild(lcDistance, argWord)
    End Sub

    Public Function Search(ByVal argWord As String, ByVal argDistance As Integer) As list(Of String)
        Dim lcList As New list(Of String)

        argWord = argWord.ToLower()

        RecursiveSearch(mRoot, lcList, argWord, argDistance)

        Return lcList

    End Function

    Private Sub RecursiveSearch(ByVal argNode As node, ByRef argList As List(Of String), ByVal argWord As String, ByVal argDistance As Integer)
        Dim lcCurrentDistance = LevenshteinDistance(argNode.Word, argWord)
        Dim lcMinDistance = lcCurrentDist - argDistance
        Dim lcMaxDistance = lcCurrentDist + argDistance

        If (lcCurrentDistance <= argDistance) Then
            argList.Add(argNode.Word)
        End If

        'For Each lckey In node.Keys.Cast<int>().Where(key => lcMinDistance <= key andalso key <= lcMaxDistance)
        '    RecursiveSearch(argNode(lckey), argList, argWord, argDistance)
        'Next

    End Sub

    Public Shared Function LevenshteinDistance(ByVal argFirst As String, ByVal argSecond As String) As Integer
        If (argFirst.Length = 0) Then Return argSecond.Length
        If (argSecond.Length = 0) Then Return argFirst.Length

        Dim lcLenFirst = argFirst.Length
        Dim lcLenSecond = argSecond.Length

        Dim lcArray = New Integer(lcLenFirst + 1, lcLenSecond + 1)

        For i As Integer = 0 To lenFirst
            lcArray(i, 0) = i
        Next

        For i As Integer = 0 To lenSecond
            lcArray(0, i) = i
        Next

        For i As Integer = 1 To lenFirst
            For j As Integer = 1 To lenSecond
                Dim lcMatch As Integer = IIF(first(i - 1) = second(j - 1), 0, 1)
                lcArray(i, j) = Math.Min(Math.Min(lcArray(i - 1, j) + 1, lcArray(i, j - 1) + 1), lcArray(i - 1, j - 1) + lcMatch)
            Next
        Next

        Return lcArray(lenFirst, lenSecond)
    End Function

End Class


Public Class Node
    Public Property Word As String
    Public Property Children As HybridDictionary

    Public Sub New()

    End Sub

    Public Sub New(ByVal argWord As String)
        Me.Word = argWord.ToLower()
    End Sub

    Public Function this(key As Integer) As node
        Return Children(key)
    End Function

    Public Property Keys As ICollection
        Get
            If (Children Is Nothing) Then Return New ArrayList()
            Return Children.Keys
        End Get
    End Property

    Public Function ContainsKey(ByVal argKey As Integer) As Boolean
        Return Children Is Not Nothing AndAlso Children.Contains(argKey)
    End Function

    Public Sub AddChild(ByVal argKey As Integer, ByVal argWord As String)
        If (Me.Children Is Nothing) Then
            Children = New HybridDictionary
            Me.Children(argKey) = New Node(argWord)
        End If
    End Sub
End Class