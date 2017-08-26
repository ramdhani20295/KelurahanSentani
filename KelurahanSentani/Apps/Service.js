angular.module("app.service", [])
    .factory("BaseUrl", function () {
        var service = {};
        service.URL = "http://localhost:52814";
        return service;
    })

    .factory("Helpers", function ()
    {
        var service = {};
        var messages = [];
        Load();

        service.getMessage = function (code,message)
        {
           angular.forEach(messages, function (value, key) {
               if (value.code == code) {
                   message = value.message;
                }
            });

           return message;
        }

      

        function Load()
        {
            messages.push({ code: 1234, message: 'Test Message' });
            messages.push({ code: 401, message: 'Anda tidak memiliki hak akses' });
            messages.push({ code: 1234, message: 'Test Message' });
            messages.push({ code: 1234, message: 'Test Message' });

        }

        return service;

    })

    .factory("PejabatService", function ($http, $q, BaseUrl,Helpers) {
        var service = {};
        service.isInstant = false;
        service.collection = [];
        service.source = function ()
        {
            deferred = $q.defer();
            if (!service.isInstant)
            {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/pejabat/get",
                }).then(function (data) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    service.collection = data;
                    deferred.resolve(service.collection);
                    service.isInstant = true;

                    }, function (error) {

                        alert(Helpers.getMessage(error.status,error.data.Message));
                   // deferred.reject(error);
                });
               
            } else
            {
                deferred.resolve(service.collection);
            }

            return deferred.promise;
        }
        service.GetPejabatRW = function ()
        {
            if (!service.isInstant)
                service.source();

            var datas = [];
            angular.forEach(service.collection, function (value, key) {
                if (value.Level == 'RW') {
                    datas.push(value);
                }
            });

        }

        service.GetPejabatRT = function () {
            if (!service.isInstant)
                service.source();

            var datas = [];
            angular.forEach(service.collection, function (value, key) {
                if (value.Level == 'RT') {
                    datas.push(value);
                }
            });

        }
        return service;
    })

    .factory("StrukturKelurahanService", function ($http, $q, BaseUrl, Helpers) {
        var service = {};
        service.isInstant = false;
        service.collection = [];
        service.source = function () {
            deferred = $q.defer();
            if (!service.isInstant) {
                $http({
                    method: 'GET',
                    url: BaseUrl.URL + "/api/StrukturKelurahan/getrw",
                }).then(function (data) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    service.collection = data;
                    deferred.resolve(service.collection);
                    service.isInstant = true;

                }, function (error) {

                    alert(Helpers.getMessage(error.status, error.data.Message));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(service.collection);
            }

            return deferred.promise;
        };

        service.Tambah = function(model)
        {
            deferred = $q.defer();
            if (!service.isInstant) {
                $http({
                    method: 'post',
                    url: BaseUrl.URL + "/api/StrukturKelurahan/postrw",
                    data: model
                }).then(function (data) {


                    service.collection.push(data);
                    deferred.resolve(service.collection);
                    service.isInstant = true;

                }, function (error) {

                    alert(Helpers.getMessage(error.status, error.data.Message));
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(service.collection);
            }

            return deferred.promise;
        }

        return service;
    })




    ;