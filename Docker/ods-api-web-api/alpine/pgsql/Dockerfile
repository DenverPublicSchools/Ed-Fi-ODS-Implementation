# SPDX-License-Identifier: Apache-2.0
# Licensed to the Ed-Fi Alliance under one or more agreements.
# The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
# See the LICENSE and NOTICES files in the project root for more information.

# Base image with additional packages
FROM mcr.microsoft.com/dotnet/aspnet:6.0.21-alpine3.17@sha256:2ff3f278d85bd13aa630943c1e624300d9863e200f16d0cab30e09d2f3b0fc8b AS base
RUN apk --no-cache add unzip=~6 dos2unix=~7 bash=~5 gettext=~0 icu=~72 curl=~8

# Start a new layer so that the above layer can be cached
FROM base AS build
RUN apk --no-cache add postgresql13-client=~13
ARG API_VERSION=7.0.1994
ARG STANDARD_VERSION

LABEL maintainer="Ed-Fi Alliance, LLC and Contributors <techsupport@ed-fi.org>"

# These variables can be overwritten at runtime
ENV POSTGRES_PORT=5432
ENV PGBOUNCER_LISTEN_PORT=6432
ENV NPG_POOLING_ENABLED=false
ENV ODS_VIRTUAL_NAME=api
ENV PATH_BASE="${ODS_VIRTUAL_NAME}"

# Alpine image does not contain Globalization Cultures library so we need to
# install ICU library to get LINQ expression to work and disable the
# globaliztion invariant mode, which is set in the base image.
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY appsettings.template.json /app/appsettings.template.json
COPY run.sh /app/run.sh
COPY log4net.config /app/log4net.txt

RUN wget -q -O /app/WebApi.zip https://pkgs.dev.azure.com/ed-fi-alliance/Ed-Fi-Alliance-OSS/_apis/packaging/feeds/EdFi/nuget/packages/EdFi.Suite3.Ods.WebApi.Standard.${STANDARD_VERSION}/versions/${API_VERSION}/content && \
    unzip /app/WebApi.zip -d /app && \
    rm -f /app/WebApi.zip && \
    cp /app/log4net.txt /app/log4net.config && \
    dos2unix /app/*.json && \
    dos2unix /app/*.sh && \
    dos2unix /app/log4net.config && \
    chmod 700 /app/*.sh -- ** && \
    rm -f /app/*.exe && \
    apk del unzip dos2unix

EXPOSE 80

ENTRYPOINT [ "/app/run.sh" ]
