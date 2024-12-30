using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day6.Part2
{
    public class BruteForceSolver
    {
        private static Direction startingDirection = Direction.up;
        private static Position startingPosition = new(0, 0);

        public static async Task Main(string[] args)
        {
            //int options = Program.Part1();
            //Console.WriteLine(options);

            BruteForceSolver solver = new BruteForceSolver();
            solver.BruteForceSynchronously();

            //BruteForceSolver solver = new BruteForceSolver();
            //await solver.Run();
        }

        private int BruteForceSynchronously()
        {
            SynchronousSolver sync = new SynchronousSolver();
            
            return sync.Solve(LoadData());

            Console.WriteLine("Generating Possibilities...");
            List<List<List<char>>> options = GeneratePossibilities(LoadData());

            Console.WriteLine("Starting solver; ");
            int output = 0;

            ConsoleSpinner spinner = new ConsoleSpinner();
            spinner.Start();

            int total = 0;
            //foreach (List<List<char>> option in options)
            //{
                spinner.Turn();

                int result = 0;
                //int result = SolverSynchronously(options[0], spinner);

                if (result == -1)
                {
                    output++;
                    spinner.Stop();
                    Console.WriteLine("Infinite found, total: " + output);
                    spinner.Start();
                }
            //}

            return output;
        }


        private async Task Run()
        {
            List<List<List<char>>> options = BruteForceSolver.GeneratePossibilities(BruteForceSolver.LoadData());
            int numbTasks = options.Count;

            Console.WriteLine("possibilities: " + numbTasks);

            //int numbTasks = 5;
            int threadsTimedOut = 0;

            List<Task<bool>> tasks = new();

            Console.Write("starting tasks: " + numbTasks);
            for (int i = 0; i < 5; i++)
            {
                Task<bool> startedtask = ExecuteWithTimeout(i, new CancellationTokenSource(), options[i]);
                tasks.Add(startedtask);
                if (i % 500 == 0) Console.Write(".");
            }
            Console.WriteLine(" done");

            Console.WriteLine("started " + numbTasks + " tasks");
            await Task.WhenAll(tasks);

            foreach (Task<bool> task in tasks)
                if (task.Result)
                    threadsTimedOut++;

            Console.WriteLine("timeout/total " + threadsTimedOut + " / " + numbTasks);
        }

        private static async Task<bool> ExecuteWithTimeout(int taskId, CancellationTokenSource cts, List<List<char>> map)
        {
            //Task timeout = Task.Delay(1000);
            //Task solver = Task.Run(() => SolverSynchronous(taskId, cts.Token, map));

            ////Console.WriteLine(" task " + taskId + " started");
            //Task completed = await Task.WhenAny(solver, timeout);

            //cts.Cancel();
            //if (completed == timeout)
            //{
            //    Console.WriteLine("timeout " + taskId);
            //    return true;
            //}


            //return false;

            return await Task.Run(() => { return Solver(taskId, cts.Token, map); });
        }

        private static async Task<bool> Solver(int id, CancellationToken token, List<List<char>> map)
        {
            Direction currentDirection = startingDirection;
            Position currentPosition = startingPosition;
            int maxSteps = map.Count * map[0].Count;
            int stepstaken = 0;

            Position nextStep = currentPosition + currentDirection;
            bool nextStepOutOfBounds = OutOfBounds(map, nextStep);
            bool nextStepIsBlocked = map[nextStep.y][nextStep.x].Equals('#');
            while (!nextStepOutOfBounds && !token.IsCancellationRequested && stepstaken < maxSteps)
            {
                while (!nextStepOutOfBounds &&
                    !nextStepIsBlocked &&
                    !token.IsCancellationRequested &&
                    stepstaken < maxSteps)
                {
                    currentPosition = nextStep;
                    stepstaken++;

                    nextStep = currentPosition + currentDirection;
                    nextStepOutOfBounds = OutOfBounds(map, nextStep);
                    nextStepIsBlocked = nextStepOutOfBounds || map[nextStep.y][nextStep.x].Equals('#');
                }

                currentDirection = Turn(currentDirection);

                nextStep = currentPosition + currentDirection;
                nextStepOutOfBounds = OutOfBounds(map, nextStep);
                nextStepIsBlocked = nextStepOutOfBounds || map[nextStep.y][nextStep.x].Equals('#');
            }

            if (maxSteps <= stepstaken) Console.WriteLine("max steps reached: " + id);
            return token.IsCancellationRequested || maxSteps <= stepstaken;
        }

        private static Direction Turn(Direction dir)
        {
            if (dir.Equals(Direction.up)) return Direction.right;
            if (dir.Equals(Direction.right)) return Direction.down;
            if (dir.Equals(Direction.down)) return Direction.left;
            if (dir.Equals(Direction.left)) return Direction.up;
            return Direction.up;
        }

        private static bool OutOfBounds(List<List<char>> map, Position pos)
        {
            if (pos.x < 0 || pos.y < 0) return true;
            if (map.Count <= pos.y || map[pos.y].Count <= pos.x)
                return true;
            return false;
        }

        private static List<List<List<char>>> GeneratePossibilities(List<List<char>> original)
        {
            List<List<List<char>>> options = new();

            for (int y = 0; y < original.Count; y++)
            {
                for (int x = 0; x < original[y].Count; x++)
                {
                    List<List<char>> alter = new();
                    foreach (List<char> row in original)
                    {
                        alter.Add(row.ToList());
                    }
                    //if (alter[y][x].Equals('^') || alter[y][x].Equals('#')) continue;
                    if (alter[y][x].Equals('^'))
                        continue;

                    alter[y][x] = '#';
                    options.Add(alter);
                }
            }

            return options;
        }

        private static List<List<char>> LoadData()
        {
            List<List<char>> data = new();
            using (StreamReader sr = DayReader.GetInputReader())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(new(line.ToCharArray()));

                    if (line.Contains('^'))
                    {
                        startingDirection = Direction.up;
                        startingPosition.x = line.IndexOf('^');
                        startingPosition.y = data.Count - 1;
                    }
                }
            }

            return data;
        }
    }
}
