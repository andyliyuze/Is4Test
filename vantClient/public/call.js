var mgr = new Oidc.UserManager({ userStore: new Oidc.WebStorageStateStore(), loadUserInfo: true, filterProtocolClaims: true });

mgr.signinRedirectCallback().then(function (user) {
    console.log('User' + user);
    window.location.href = '../';
}).catch(function (err) {
    console.log(err);
});
