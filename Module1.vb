Module Module1

    Sub Main()
        Dim line As String
        Dim listnames As New List(Of String)
        Dim listsurnames As New List(Of String)
        Dim bk = New BKTree()

        Dim file1 As New System.IO.StreamReader("c:\names.txt")

        Do
            line = file1.ReadLine()
            listnames.Add(line)
        Loop Until line Is Nothing

        file1.Close()

        Dim file2 As New System.IO.StreamReader("c:\surnames.txt")
        Do
            line = file2.ReadLine()
            listsurnames.Add(line)
        Loop Until line Is Nothing

        file2.Close()


        For i As Integer = 0 To 1000
            For j As Integer = 0 To 1000
                bk.Add(listnames(i) + listsurnames(j))
            Next
        Next

        bk.Add("fetullahgülen")
        bk.Add("firatkaptan")

        Console.WriteLine("Ready!")

        While (True)
            Dim l = Console.ReadLine()

            Dim stopWatch As New Stopwatch()
            stopWatch.Start()

            Dim x = (l.Length * 15 / 100) + 1
            Dim a = bk.Search(l, x)
            stopWatch.Stop()
            Dim ts As TimeSpan = stopWatch.Elapsed

            Dim elapsedTime As String = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10)

            Console.WriteLine("RunTime " + elapsedTime)
            Console.WriteLine(String.Join(", ", a.ToArray()))
        End While
    End Sub

End Module
