using MyOwnGame.Models;
using MyOwnGame.Mvvm;
using MyOwnGame.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyOwnGame.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        #region Fields
        private ICommand _openAuctionPopup;
        private ICommand _openAllPopup;
        private ICommand _openPopUp;
        private ICommand _closeAuctionPopup;
        private ICommand _clouseAllPopup;

        private bool _allPopup;
        private bool _auctionPopup;
        private Question _question;
        #endregion 


        public TabViewModel(string name, IEnumerable<Category> categories)
        {
            Name = name;
            Categories = new ObservableCollection<Category>(categories);
        }


        #region Properties
        public string Name { get; set; }

        public ObservableCollection<Category> Categories { get; set; }

        public Question ItemQuestion
        {
            get => _question;
            set => Set(ref _question, value);
        }

        public bool AuctionPopup
        {
            get => _auctionPopup;
            set => Set(ref _auctionPopup, value);
        }

        public bool AllPopup
        {
            get => _allPopup;
            set => Set(ref _allPopup, value);
        }
        #endregion


        #region Commands
        public ICommand OpenAuctionPopup => _openAuctionPopup ??
           (_openAuctionPopup = new DelegateCommand(() => AuctionPopup = true));

        public ICommand OpenAllPopup => _openAllPopup ??
           (_openAllPopup = new DelegateCommand(() => AllPopup = true));

        public ICommand CloseAuctionPopup => _closeAuctionPopup ??
           (_closeAuctionPopup = new DelegateCommand(() =>
           {
               AuctionPopup = false;
               AllPopup = true;
           }));

        public ICommand ClouseAllPopup => _clouseAllPopup ??
           (_clouseAllPopup = new DelegateCommand(() =>
           {
               AllPopup = false;
               ItemQuestion.IsNotAnswered = false; 
           }));

        public ICommand OpenPopUp => _openPopUp ??
            (_openPopUp = new DelegateCommand<Question>(q =>
            {
                ItemQuestion = q;
                if (q.Tag == QuestionType.Auction)
                {
                    AuctionPopup = true;
                }
                else
                {
                    AllPopup = true;
                }
            }));
        #endregion 
    }
}