using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private BasketDbContext _db;


        public MainWindowViewModel()
        {
            _db = new BasketDbContext();
            SelectProductCommand = new RelayCommand(LoadPircejsBySelectedProduct);
            SearchCommand = new RelayCommand(FilterData);
            DeleteCommand = new RelayCommand(DeleteSelectedProduct, CanDeleteProduct);
// SaveCommand = new RelayCommand(SaveProduct, CanSaveProduct);
            SavePircejsCommand = new RelayCommand(SavePircejs, CanSavePircejs);

        }

        private Product[] _products;
        public Product[] Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private Pircejs _selectedPircejs;
public Pircejs SelectedPircejs
{
    get => _selectedPircejs;
    set
    {
        _selectedPircejs = value;
        OnPropertyChanged();

        if (value != null)
        {
            EditPircejs = new Pircejs
            {
                Id = value.Id,
                Name = value.Name,
                Surname = value.Surname
            };
        }

        (SavePircejsCommand as RelayCommand)?.NotifyCanExecuteChanged();
    }
}


        private List<Pircejs> _pirceji;

        private List<Pircejs> _allPirceji;

        public List<Pircejs> Pirceji
        {
            get => _pirceji;
            set
            {
                _pirceji = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectProductCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SavePircejsCommand { get; }



        public void Load()
        {
            Products = _db.Products.ToArray();

            _allPirceji = _db.Pircejs.ToList();
            Pirceji = _allPirceji;

            Categories = _db.Products
                .Select(p => p.ProductCategory)
                .Distinct()
                .ToList();
        }



        private void LoadPircejsBySelectedProduct()
        {
            Pirceji = _db.Pircejs.ToList();
        }

        private string _searchSurname;
        public string SearchSurname
        {
            get => _searchSurname;
            set
            {
                _searchSurname = value;
                OnPropertyChanged();
            }
        }


        public void FilterData()
        {
            IEnumerable<Pircejs> filtered = _allPirceji;

            if (!string.IsNullOrWhiteSpace(SearchSurname))
            {
                string s = SearchSurname.ToLower();
                filtered = filtered.Where(p => p.Surname.ToLower().Contains(s));
            }
            Pirceji = filtered.ToList();
        }



        private List<string> _categories;
        public List<string> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                FilterData();      
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
                (DeleteCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (SaveCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }


        private bool CanDeleteProduct()
        {
            return SelectedProduct != null;
        }

        private void DeleteSelectedProduct()
        {
            if (SelectedProduct == null)
                return;

            _db.Products.Remove(SelectedProduct);
            _db.SaveChanges();

            Load();
        }

        private Pircejs _editPircejs;
        public Pircejs EditPircejs
        {
            get => _editPircejs;
            set => SetProperty(ref _editPircejs, value);
        }


        private bool CanSavePircejs()
        {
            return EditPircejs != null && SelectedPircejs != null;
        }

        private void SavePircejs()
        {
            if (SelectedPircejs == null || EditPircejs == null)
                return;

            SelectedPircejs.Name = EditPircejs.Name;
            SelectedPircejs.Surname = EditPircejs.Surname;

            _db.Pircejs.Update(SelectedPircejs);
            _db.SaveChanges();

            Load();
        }

    }

}
