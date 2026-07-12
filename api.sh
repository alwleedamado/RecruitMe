#!/usr/bin/env bash

set -e

PROJECT="backend/src/RecruitMe.Api"

echo "Scaffolding RecruitMe.Api..."

mkdir -p "$PROJECT/Authorization/Policies"
mkdir -p "$PROJECT/Authorization/Requirements"
mkdir -p "$PROJECT/Configuration"
mkdir -p "$PROJECT/Constants"
mkdir -p "$PROJECT/Controllers"
mkdir -p "$PROJECT/Endpoints"
mkdir -p "$PROJECT/ExceptionHandlers"
mkdir -p "$PROJECT/Extensions"
mkdir -p "$PROJECT/Filters"
mkdir -p "$PROJECT/HealthChecks"
mkdir -p "$PROJECT/Middleware"
mkdir -p "$PROJECT/Properties"
mkdir -p "$PROJECT/Responses"
###########################################
# Configuration
###########################################

touch "$PROJECT"/Configuration/CorsConfiguration.cs
touch "$PROJECT"/Configuration/IdentityConfiguration.cs
touch "$PROJECT"/Configuration/JwtConfiguration.cs
touch "$PROJECT"/Configuration/OpenApiConfiguration.cs
touch "$PROJECT"/Configuration/SerilogConfiguration.cs
touch "$PROJECT"/Configuration/HealthChecksConfiguration.cs

###########################################
# Extensions
###########################################

touch "$PROJECT"/Extensions/ServiceCollectionExtensions.cs
touch "$PROJECT"/Extensions/ApplicationBuilderExtensions.cs

###########################################
# Exception Handler
###########################################

touch "$PROJECT"/ExceptionHandlers/GlobalExceptionHandler.cs

###########################################
# Authorization
###########################################

touch "$PROJECT"/Authorization/Policies/PolicyNames.cs
touch "$PROJECT"/Authorization/Requirements/PermissionRequirement.cs

###########################################
# Controllers
###########################################

touch "$PROJECT"/Controllers/AuthenticationController.cs

###########################################
# Constants
###########################################

touch "$PROJECT"/Constants/ApiConstants.cs
touch "$PROJECT"/Constants/Roles.cs
touch "$PROJECT"/Constants/Policies.cs

###########################################
# Responses
###########################################

touch "$PROJECT"/Responses/ValidationProblemResponse.cs

###########################################
# Health Checks
###########################################

touch "$PROJECT"/HealthChecks/DatabaseHealthCheck.cs

###########################################
# Middleware
###########################################

touch "$PROJECT"/Middleware/RequestLoggingMiddleware.cs

###########################################
# Filters
###########################################

touch "$PROJECT"/Filters/ValidateModelFilter.cs

###########################################
# Endpoints
###########################################

touch "$PROJECT"/Endpoints/EndpointMappings.cs

echo
echo "RecruitMe.Api scaffold created successfully."