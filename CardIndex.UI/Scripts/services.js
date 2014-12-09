'use strict';

// Demonstrate how to register services
// In this case it is a simple value service.
angular.module('app.services', ['ngResource'])
.factory('BookApi', ['$resource', function ($resource) {
    return $resource('http://localhost:1605/api/book', null, {
     
    });
}]);
