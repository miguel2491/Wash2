﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wash2.Models;
using Xamarin.Forms;

namespace Wash2.ViewModels
{
    public class MonkeysViewModel : INotifyPropertyChanged
    {

        readonly IList<Monkey> source;

        public ObservableCollection<Monkey> Monkeys { get; private set; }
        public IList<Monkey> EmptyMonkeys { get; private set; }

        public Monkey PreviousMonkey { get; set; }
        public Monkey CurrentMonkey { get; set; }
        public Monkey CurrentItem { get; set; }
        public int PreviousPosition { get; set; }
        public int CurrentPosition { get; set; }
        public int Position { get; set; }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        public ICommand ItemChangedCommand => new Command<Monkey>(ItemChanged);
        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        public ICommand DeleteCommand => new Command<Monkey>(RemoveMonkey);
        public ICommand FavoriteCommand => new Command<Monkey>(FavoriteMonkey);


        public MonkeysViewModel()
        {
            source = new List<Monkey>();
            CreateMonkeyCollection();

            CurrentItem = Monkeys.Skip(3).FirstOrDefault();
            OnPropertyChanged("CurrentItem");
            Position = 3;
            OnPropertyChanged("Position");
        }

        void CreateMonkeyCollection()
        {
            source.Add(new Monkey
            {
                Name = "SOMOS",
                TituloD2 = "",
                TituloD3 = "",
                Subname = "WASH DRY APP",
                Titulo = "BIENVENIDO",
                SubTitulo = "",
                Sub = ""
            });

            source.Add(new Monkey
            {
                Name = "",
                TituloD2 = "GANA DINERO CON NOSOTROS",
                TituloD3 = "EN WASH DRY PUEDES GENERAR PUEDES GENERAR INGRESOS TRABAJANDO COMO WASHER (LAVADOR DE AUTOS)",
                Subname = "",
                Titulo = "ES MUY SENCILLO, REGÍSTRATE Y ESPERA NUESTRA LLAMADA",
                SubTitulo = "",
                Sub = ""
            });

            source.Add(new Monkey
            {
                Name = "",
                TituloD2 = "UNA VEZ QUE RECIBAS NUESTRA LLAMADA Y SIGAS EL PROCESO ESTARÁS LISTO PARA SER PARTE DE NUESTRA FAMILIA.",
                TituloD3 = "WASH DRY APP LA MEJOR APLICACIÓN DE LAVADO DE AUTO A DOMICILIO.",
                Subname = "",
                Titulo = "",
                SubTitulo = "",
                Sub = ""
            });

            Monkeys = new ObservableCollection<Monkey>(source);
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where(monkey => monkey.Name.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var monkey in source)
            {
                if (!filteredItems.Contains(monkey))
                {
                    Monkeys.Remove(monkey);
                }
                else
                {
                    if (!Monkeys.Contains(monkey))
                    {
                        Monkeys.Add(monkey);
                    }
                }
            }
        }

        void ItemChanged(Monkey item)
        {
            PreviousMonkey = CurrentMonkey;
            CurrentMonkey = item;
            OnPropertyChanged("PreviousMonkey");
            OnPropertyChanged("CurrentMonkey");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        void RemoveMonkey(Monkey monkey)
        {
            if (Monkeys.Contains(monkey))
            {
                Monkeys.Remove(monkey);
            }
        }

        void FavoriteMonkey(Monkey monkey)
        {
            monkey.IsFavorite = !monkey.IsFavorite;
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
