roles.service('roleService', ['$http', '$q', 'apiService', function ($http, $q, apiService) {

    var roleService = {};

    var createRoles = function (Role) {        
        var deferred = $q.defer();
        console.log("Role", Role);
        apiService.create("createRole", Role).then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    var updateRoles = function (Role) {
        var deferred = $q.defer();
        console.log("Role", Role);
        apiService.update("updaterole", Role).then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    var getRoles = function () {
        var deferred = $q.defer();
        apiService.get("allroles").then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    var getRolesDetails = function (id) {
        var deferred = $q.defer();
        apiService.get("allroles/" + id).then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    var deleteRole = function (id) {
        var deferred = $q.defer();
        apiService.delet("deleterole" , id).then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    roleService.createRoles = createRoles;
    roleService.updateRoles = updateRoles;
    roleService.getRoles = getRoles;
    roleService.getRolesDetails = getRolesDetails;
    roleService.deleteRole = deleteRole;

    return roleService;

}]);