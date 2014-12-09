'use strict';

// Demonstrate how to register services
// In this case it is a simple value service.
angular.module('app.services', ['ngResource'])
    .factory('BookApi', [
        '$http', function (async) {
            return {
                getBooks: function () {
                    var promise = async({ method: 'JSONP', url: 'http://localhost:1605/api/book' })
                        .success(function (data, status, headers, config) {
                            return data;
                        })
                        .error(function (data, status, headers, config) {
                            return { "status": false };
                        });
                }
            };
        }
    ]);
