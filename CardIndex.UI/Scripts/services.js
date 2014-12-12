'use strict';

// Demonstrate how to register services
// In this case it is a simple value service.
angular.module('app.services', ['ngResource'])
    .factory('BookApi', [
        '$resource', function ($resource) {
            return $resource('http://localhost:1605/api/book', null, {
                update: {
                    method: 'PUT',
                },
                save: {
                    method: 'POST'
                }
            });
        }
    ])
    .factory('GenreApi', [
        '$resource', function ($resource) {
            return $resource('http://localhost:1605/odata/Genre(:key)', { key: '@id' }, {
                update: {
                    method: 'PUT',
                },
                save: {
                    method: 'POST'
                }
            });
        }
    ])
    .factory('AuthorApi', [
        '$resource', function ($resource) {
            return $resource('http://localhost:1605/odata/Author(:key)', { key: '@id' }, {
                upWdate: {
                    method: 'PUT',
                },
                save: {
                    method: 'POST'
                }
            });
        }
    ]);
