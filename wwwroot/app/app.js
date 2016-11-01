(function(){
  'use strict';

  function config($stateProvider, $urlRouterProvider){
      $stateProvider
        .state('home', {
          url: '/', 
          templateUrl: '/app/home/views/home.view.html'
        })
        .state('todo', {
          url: '/todo',
          templateUrl: '/app/todo/views/todo.view.html'
        });

      $urlRouterProvider.otherwise('/');
  }

  angular.module('ToDoApp', ['ngCookies', 'ngResource', 'ngSanitize', 'ui.router'])
    .config(['$stateProvider', '$urlRouterProvider', config]);
}())