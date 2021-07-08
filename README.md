# Membership

用于AspNet.Mvc、AspNet.WebApi和AspNetCore.Mvc的Membership（用户、角色、权限）。

- #### Liyanjie.Membership.Abstractions
  - Authority
  - AuthorityGroupAttribute
  - AuthorityOptions&lt;TAuthorityProvider, TAuthorityDescriptor&gt;
  - IAuthorityProvider
  - IRole
  - IUser
  - Membership&lt;TAuthorityProvider&gt;

- #### Liyanjie.Membership.AspNet.Mvc.ActionPath
  - Useage
    ```csharp
    //services is IServiceCollection
    services.AddMembership<TMembershipImplementation, TAuthorityProvider>(
        Action<ActionPathAuthorityOptions> authorityOptionsConfigure = null)
        where TMembershipImplementation : Membership<ActionPathAuthorityProvider>
        where TAuthorityProvider : ActionPathAuthorityProvider
    ```
- #### Liyanjie.Membership.AspNet.WebApi.HttpMethod
  - Useage
    ```csharp
    //services is IServiceCollection
    services.AddMembership<TMembershipImplementation, TAuthorityProvider>(
        Action<HttpMethodAuthorityOptions> authorityOptionsConfigure = null)
        where TMembershipImplementation : Membership<HttpMethodAuthorityProvider>
        where TAuthorityProvider : HttpMethodAuthorityProvider
    ```
- #### Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
  - Useage
    ```csharp
    //services is IServiceCollection
    services.AddMembership<TMembershipImplementation, TAuthorityProvider>(
        Action<ActionPathAuthorityOptions> authorityOptionsConfigure = null)
        where TMembershipImplementation : Membership<ActionPathAuthorityProvider>
        where TAuthorityProvider : ActionPathAuthorityProvider
    ```
- #### Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
  - Useage
    ```csharp
    //services is IServiceCollection
    services.AddMembership<TMembershipImplementation, TAuthorityProvider>(
        Action<HttpMethodAuthorityOptions> authorityOptionsConfigure = null)
        where TMembershipImplementation : Membership<HttpMethodAuthorityProvider>
        where TAuthorityProvider : HttpMethodAuthorityProvider
    ```