System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
sw.Start();

AStar a = new AStar();

sw.Stop();

Debug.Log (sw.Elapsed.TotalMilliseconds);
Debug.Log (sw.Elapsed.TotalSeconds);