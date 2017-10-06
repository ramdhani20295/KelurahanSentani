angular.module('app', ['app.service', 'app.controller', 'ngRoute', 'ngAutocomplete'])
.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "../Apps/Views/main.html",
        })
        .when("/pejabat", {
            templateUrl: "../Apps/Views/pejabat.html",
            controller: "PejabatController"
        })
        .when("/staff", {
            templateUrl: "../Apps/Views/staff.html",
        })
        .when("/rw", {
            templateUrl: "../Apps/Views/rw.html",
            controller:"StrukturKelurahanController"
        })
        .when("/pindah", {
            templateUrl: "../Apps/Views/pindah.html",
            controller: "SuratPindahController"
        })
        .when("/kematian", {
            templateUrl: "../Apps/Views/kematian.html",
            controller:"SuratKematianController"
        })
        .when("/umum", {
            templateUrl: "../Apps/Views/umum.html",
            controller:"SuratUmumController"
        })
        .when("/kependudukan", {
            templateUrl: "../Apps/Views/kependudukan.html",
        })
        .when("/pmasuk", {
            templateUrl: "../Apps/Views/pmasuk.html",
        })
        .when("/pkeluar", {
            templateUrl: "../Apps/Views/pkeluar.html",
        })
        .when("/kelahiran", {
            templateUrl: "../Apps/Views/kelahiran.html",
        })
        .when("/penduduk", {
            templateUrl: "../Apps/Views/penduduk.html",
            controller:"PendudukController"
        })
        .when("/kk", {
            templateUrl: "../Apps/Views/kk.html",
            controller: "KartuKeluargaController"
        })
        .when("/kkanggota", {
            templateUrl: "../Apps/Views/kkanggota.html",
            controller: "KartuKeluargaDetailController"
        })
        .when("/permohonan", {
            templateUrl: "../Apps/Views/permohonan.html",
            controller: "PermohonanController"
        })
        .when("/ReportUmum/:Id", {
            templateUrl: "../Apps/Views/ReportUmum.html",
            controller: "ReportUmumController"
        })  
        .when("/ReportPindah/:Id", {
            templateUrl: "../Apps/Views/ReportPindah.html",
            controller: "ReportPindahController"
        })
        .when("/ReportKematian/:Id", {
            templateUrl: "../Apps/Views/ReportKematian.html",
            controller: "ReportKematianController"
        })
})

.controller("RedController", function ($scope) {

    $scope.RW= [];

    $scope.Init=function()
    {
        var rw ={};
        rw.Nama="I";
        rw.RT=[];
        
        var rt1 = {};
        rt1.Nama="1";

        var rt2 = {};
        rt2.Nama="2";


        rw.RT.push(rt1);
        rw.RT.push(rt2);

        $scope.RW.push(rw);
        $scope.RW.push({ 'Nama': 'II' });

    }

    $scope.ShowName=function(model)
    {
        $scope.RW.push(model);
    }

   


})







    ;