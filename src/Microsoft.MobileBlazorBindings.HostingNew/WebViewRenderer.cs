﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.HostingNew
{
    internal class WebViewRenderer : Renderer
    {
        private readonly Action<Exception> _onException;
        private readonly Dispatcher _dispatcher;

        public WebViewRenderer(
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            Action<Exception> onException,
            Dispatcher dispatcher)
            : base(serviceProvider, loggerFactory)
        {
            _onException = onException ?? throw new ArgumentNullException(nameof(onException));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public override Dispatcher Dispatcher => _dispatcher;

        public Task AddRootComponentAsync(Type componentType, string domElementSelector, ParameterView parameters)
        {
            var component = InstantiateComponent(componentType);
            var componentId = AssignRootComponentId(component);

            // TODO: At this point, notify the JS runtime that it should associate
            // componentId with selector so the following renderbatch will work

            return RenderRootComponentAsync(componentId, parameters);
        }

        protected override void HandleException(Exception exception)
            => _onException(exception);

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            throw new NotImplementedException();
        }
    }
}