using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using BuildingBlocks.Models;
using Caliburn.Micro;
using static System.Math;
namespace BuildingBlocks.Presentation.ViewModels
{
    public class AlgorithmSimulationViewModel : Screen
    {
        public ObservableCollection<Simulation> Simulations { get; set; }

        private readonly List<Block> _blocks;

        private readonly int _boardWidth;

        private readonly int _k;

        private int _step;

        private readonly int _canvasWidth = 600;
        private readonly int _canvasHeight = 600;

        private readonly int _blockWidth;
        private volatile bool flag = true;
        private ConcurrentBag<Thread> _computationThreadsPool { get; set; } = new ConcurrentBag<Thread>();


        public AlgorithmSimulationViewModel(List<Block> blocks, int boardWidth, int k, int step)
        {
            _blocks = blocks;
            _boardWidth = boardWidth;
            _k = k;
            _step = step;
            if (_boardWidth > 50)
            {
                _canvasWidth = _boardWidth * 10;
            }
            _blockWidth = _canvasWidth / _boardWidth;
            Simulations = new ObservableCollection<Simulation>();
            for (var i = 0; i < _k; ++i)
            {
                Simulations.Add(new Simulation
                {
                    CanvasChildren = new ObservableCollection<Rectangle>(),
                    AvailableBlocks = _blocks
                });
                //Block[] tmp= new Block[_blocks.Count];
                //_blocks.CopyTo(tmp);
                //Simulations.Last().AvailableBlocks = tmp.ToList();
                //foreach (var el in Simulations.Last().AvailableBlocks)
                //{
                //    Rectangle[] tmp2 = new Rectangle[el.CanvasChildren.Count];
                //    el.CanvasChildren.CopyTo(tmp2);
                //    el.CanvasChildren = tmp2.ToList();
                //        //foreach (var canvas in el.CanvasChildren)
                //        //{
                //        //    Canvas.SetTop(canvas, _canvasHeight - Max(canvas.Height, canvas.Width));
                //        //    Canvas.SetLeft(canvas, Max(canvas.Height, canvas.Width));
                //        //}
                //}
                var j = i;
                _computationThreadsPool.Add(new Thread(() => Computations(Simulations[j])));
            }

        }


        private void Computations(Simulation simulation)
        {
            Task.Run(() =>
            {
                for (var i = 0; i < 1e+300; i++)
                {
                    if (flag)
                    {
                        simulation.CanvasChildren = new ObservableCollection<Rectangle>();
                        Random rnd = new Random();
                        var count = simulation.AvailableBlocks.Count;
                        if (count == 0) continue;
                        int r = rnd.Next(count);
                        foreach (var element in simulation.AvailableBlocks[r].CanvasChildren)
                        {
                            simulation.CanvasChildren.Add(element);
                        }
                    }
                }
            });
            
        }

        public void Start()
        {
            flag = true;
            foreach (var thread in _computationThreadsPool)
            {
                try
                {
                    if (thread.ThreadState == ThreadState.StopRequested || thread.ThreadState == ThreadState.Stopped)
                    {
                        thread.Start();
                    }
                    else
                    {
                        thread.Start();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Thread {thread.ManagedThreadId} caused error when started.");
                }
                Console.WriteLine(thread.IsAlive.ToString() + thread.ThreadState.ToString());

                Console.WriteLine($"Thread {thread.ManagedThreadId} has been started.");
            }
            Console.WriteLine($"All computation threads have been started.");
        }

        public void Stop()
        {
            foreach (var thread in _computationThreadsPool)
            {
                try
                {
                    if (thread.ThreadState == ThreadState.Running)
                    {
                        thread.Abort();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Thread {thread.ManagedThreadId} caused error when stopped.");
                }
                Console.WriteLine($"Thread {thread.ManagedThreadId} has been stopped.");
            }
            Console.WriteLine($"All computation threads have been stopped.");
            _computationThreadsPool = new ConcurrentBag<Thread>();
        }

        public void Pause()
        {
            foreach (var thread in _computationThreadsPool)
            {
                try
                {
                    Console.WriteLine(thread.IsAlive.ToString() + thread.ThreadState.ToString());
                    if (thread.IsAlive && thread.ThreadState == ThreadState.Running)
                    {
                        // nie umiem zapauzować wątku to robie takie cos
                        flag = false;
                        //Thread.Sleep(Timeout.Infinite);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Thread {thread.ManagedThreadId} caused error when paused.");
                }
                Console.WriteLine($"Thread {thread.ManagedThreadId} has been paused.");
            }
            Console.WriteLine($"All computation threads have been paused.");
        }

        public void Next()
        {
            //throw new System.NotImplementedException();
        }
    }
}