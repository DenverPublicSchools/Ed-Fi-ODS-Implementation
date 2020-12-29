// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using Autofac;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Constants;
using EdFi.Ods.Common.Container;

namespace EdFi.Ods.Features.OwnershipBasedAuthorization.Container.Modules
{
    public class OwnershipBasedAuthorizationModule : ConditionalModule
    {
        public OwnershipBasedAuthorizationModule(ApiSettings apiSettings)
            : base(apiSettings, nameof(OwnershipBasedAuthorizationModule)) { }

        public override bool IsSelected() => IsFeatureEnabled(ApiFeature.OwnershipBasedAuthorization);

        public override void ApplyConfigurationSpecificRegistrations(ContainerBuilder builder)
        {
        }
    }
}