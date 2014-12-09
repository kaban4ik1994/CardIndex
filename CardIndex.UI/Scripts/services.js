﻿'use strict';

// Demonstrate how to register services
// In this case it is a simple value service.
angular.module('app.services', ['ngResource'])
    .factory('BookApi', [
        '$resource', function ($resource) {
            return $resource('http://localhost:1605/api/book', null, {

            });
        }
    ])
    .factory('GenreApi', [
        '$resource', function ($resource) {
            return $resource('http://localhost:1605/api/genre', null, {
            });
        }
    ])
    .factory('AuthorApi', [
        '$resource', function ($resource) {
            return $resource('http://localhost:1605/api/author', null, {
            });
        }
]);
