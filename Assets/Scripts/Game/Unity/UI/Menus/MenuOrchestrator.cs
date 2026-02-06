using System.Collections.Generic;
using UnityEngine;
using Primitives.Unity.UI.Menus;
using Primitives.Common.State.Services;
using Primitives.Execution;
using Primitives.Unity.Infrastructure;
using Primitives.Common.Execution;
using Game.Application.UI.Menus.Abstractions;
using Game.Application.UI.Menus;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Menus.Commands;

namespace Primitives.UI.Menus.Unity
{
    [RequireComponent(typeof(MenuRouter))]
    public class MenuOrchestrator : MonoBehaviour, IUICommandHandler, IInitializable<IGameContext>
    {
        public int Priority => 0;
        private MenuController _menuController;
        private MenuRouter _menuRouter;

        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            SceneInitializationRegistry.Register(this);

            // Get the pages in this menu
            Dictionary<IPageToken, IMenuPage> _pageDict = GetMenuPages();

            // Wire the commands
            foreach (IMenuPage page in _pageDict.Values)
            {
                WireCommands(page);
            }

            _menuRouter = GetComponent<MenuRouter>();
            _menuController = new MenuController(_pageDict);

        }

        private void Start()
        {
            // Once everything is settled, set the new page
            // _menuController.EnterStartPage();
        }

        public void Initialize(IGameContext context)
        {
            _menuController.AddServices(context.GameStateServices, context.QuitServices);
        }

        public void PostInitialize(IGameContext context)
        {
            // Should I break up these initialize and post initialize methods?
        }

        private void OnEnable()
        {
            _menuRouter.OnCommand += HandleCommand;
        }

        private void OnDisable()
        {
            _menuRouter.OnCommand -= HandleCommand;
        }


        public void HandleCommand(IUICommand command)
        {
            Debug.Log($"Orchestrator got command: {command.CommandType}");
            _menuController.ExecuteCommand(command);
        }

        private Dictionary<IPageToken, IMenuPage> GetMenuPages()
        {
            // Get all of the page builders
            IPageBuilder[] builders = GetComponentsInChildren<IPageBuilder>();

            // Build the pages
            Dictionary<IPageToken, IMenuPage> pageDict = new Dictionary<IPageToken, IMenuPage>();
            foreach (IPageBuilder builder in builders)
            {
                // Build the menu page
                IMenuPage menuPage = builder.Build();
                IPagePresenter pagePresenter = menuPage.PagePresenter;

                // Build the directory
                pageDict.TryAdd(pagePresenter.Token, menuPage);

            }
            Debug.Log($"Got {pageDict.Count} pages");

            return pageDict;
        }

        private void WireCommands(IMenuPage page)
        {

            // Wire the buttons
            foreach (IUIElement element in page.Elements)
            {
                element.OnSubmit += HandleCommand;
                element.OnFocus += HandleCommand;
            }

        }


    }
}