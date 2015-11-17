using System;
using System.Collections.Generic;

namespace MailingServiceDemo.Database
{
    public interface IViewModelSet<TViewModel> : IEnumerable<TViewModel> where TViewModel : ViewModel
    {
        void Add(TViewModel view);

        void Update(Guid id, Action<TViewModel> updateAction);

        void Remove(Guid id);

        TViewModel GetById(Guid id);
    }
}
