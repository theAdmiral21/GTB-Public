using UnityEngine;
using System;
using Primitives.UI.Menus.Unity.Presenters.Abstractions;
using Game.Application.UI.Menus.Abstractions;
using Game.UI.Menus.Unity.Presenters.Pages.DataStructures;
using Primitives.UI.Menus.Unity.Presenters.Components;

namespace Game.UI.Menus.Unity.Presenters.Components
{
    public class UINavigationButton : BaseSelectable
    {
        [SerializeField] private PageToken _token;

        public override Type PresenterType => typeof(UIButtonPresenter);


    }

}