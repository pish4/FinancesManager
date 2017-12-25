$(document).ready(function() {

function Account(data) {
  this.id = ko.observable(data.id);
  this.name = ko.observable(data.name);
  this.owner = ko.observable(data.owner);
}
function SharedAccount(data) {
  this.id = ko.observable(data.id);
  this.name = ko.observable(data.name);
}

function Transaction(data) {
  this.id = ko.observable(data.id);
  this.name = ko.observable(data.name);
  this.amount = ko.observable(data.amount);
}

function User(data) {
    this.username = ko.observable(data.username);
}

function getHeaders() {
    // If we already have a bearer token, set the Authorization header.
    var token = sessionStorage.getItem('tokenKey');
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }
    return headers;
}

function AppViewModel() {
  var self = this;

  self.currentUser = ko.observable("");
 
  self.accounts = ko.observableArray([]);
  self.sharedAccounts = ko.observableArray([]);
  self.transactions = ko.observableArray([]);
  self.accountUsers = ko.observableArray([]);

  self.accountSummary = ko.observable(0);

  self.newAccountName = ko.observable("");
  self.newSharedAccountName = ko.observable("");

  self.newTransactionName = ko.observable("");
  self.newTransactionAmount = ko.observable(0);

  self.chosenHome = ko.observable(null);
  self.chosenAccountInfo = ko.observable(null);
  self.chosenAccountInfoData = ko.observable(null);
  self.chosenAddAccount = ko.observable(null);
  self.chosenAddTransaction = ko.observable(null);
  self.chosenAddSharedAccount = ko.observable(null);
  self.chosenEditAccount = ko.observable(null);

  self.setCurrentUser = function() {
      var username = sessionStorage.getItem('username');
      self.currentUser(username);
  }

    self.addAccountSubmit = function (e) {
        $.ajax({
            url: '/api/FinancialAccount',
            type: 'POST',
            headers: getHeaders(),
            data: {
                name: self.newAccountName
            },
            success: function (data) {
                self.setCurrentUser();
                self.newAccountName("");
                location.hash = "#/";
            },
            error: function (error) {
                alert(error);
            }
        });
    };

    self.editAccount = function (e) {
        $.ajax({
            url: '/api/FinancialAccount/' + e.id,
            type: 'PUT',
            data: {
                name: e.name
            },
            headers: getHeaders(),
            success: function () {
                self.setCurrentUser();
                location.hash = "#/account/" + e.id;
            },
            error: function(error) {
            }
        });
    }

  self.deleteAccount = function(e) {
    $.ajax({
        url: '/api/FinancialAccount/' + e.id,
        type: 'DELETE',
        headers: getHeaders(),
        success: function () {
            self.setCurrentUser();
          location.hash = "#/";
        },
        error: function(error) {

        }
    });
  }

  self.addSharedAccountSubmit = function (e) {
      $.ajax({
          url: '/api/AccountMember',
          type: 'POST',
          headers: getHeaders(),
          data: {
              id: self.chosenAccountInfoData().id,
              name: self.newSharedAccountName
          },
          success: function (data) {
              self.setCurrentUser();
              self.newSharedAccountName("");
              location.hash = "#/account/" + self.chosenAccountInfoData().id
          },
          error: function (errir) {
              console.error(error);
          }
      });
  };
  self.addTransactionSubmit = function (e) {
      $.ajax({
          url: '/api/Transaction',
          type: 'POST',
          headers: getHeaders(),
          data: {
              name: self.newTransactionName,
              amount: self.newTransactionAmount,
              account_id: self.chosenAccountInfoData().id
          },
          success: function (data) {
              self.setCurrentUser();
              self.newTransactionName("");
              self.newTransactionAmount(0);
              location.hash = "#/account/" + self.chosenAccountInfoData().id
          },
          error: function (data) {
              alert(data)
          }
      });
  };


  self.goToAddTransaction = function(acc) {
    location.hash = "#/transaction/" + acc.id
  }

  self.goToEditAccount = function(acc) {
    location.hash = "#/account/edit/" + acc.id
  }

  self.goToAccountDetails = function(acc) {
    location.hash = "#/account/" + acc.id();
  }

  Sammy("#main", function () {
    this.get('#/', function (context) {
      self.chosenAddAccount(null);
      self.chosenAccountInfo(null);
      self.chosenAccountInfoData(null);
      self.chosenAddTransaction(null);
      self.chosenAddSharedAccount(null);
      self.chosenEditAccount(null);
      $.ajax({
          url: '/api/Account/Home',
          type: 'GET',
          headers: getHeaders(),
          success: function (json) {
              self.setCurrentUser();
              self.chosenHome("home");
              var mappedAccounts = $.map(json.accounts, function (item) { return new Account(item) });
              self.accounts(mappedAccounts);
              var mappedSharedAccounts = $.map(json.shared_accounts, function (item) { return new SharedAccount(item) });
              self.sharedAccounts(mappedSharedAccounts);
          },
          error: function (error) {
              self.chosenHome(null);
              context.partial('../static/views/about.html').then(function () { });
          }
      });
        
    });
    this.get('#/add_account', function (context) {
        self.chosenHome(null);
        self.chosenAccountInfo(null);
        self.chosenAddTransaction(null);
        self.chosenAddSharedAccount(null);
        self.chosenEditAccount(null);
        $.ajax({
            url: '/api/Account/Home',
            type: 'GET',
            headers: getHeaders(),
            success: function (json) {
                self.setCurrentUser();
                self.chosenAddAccount("add");
            }
        });
    });
    this.get('#/add_shared_account', function (context) {
        self.chosenHome(null);
        self.chosenAccountInfo(null);
        self.chosenAddTransaction(null);
        self.chosenAddAccount(null);
        self.chosenEditAccount(null);
        self.chosenAddSharedAccount("add");
    });
    this.get('#/account/:id', function(context) {
        self.chosenHome(null);
        self.chosenAddAccount(null);
        self.chosenAddTransaction(null);
        self.chosenAddSharedAccount(null);
        self.chosenEditAccount(null);
        
        $.ajax({
            url: '/api/FinancialAccount/' + context.params.id,
            type: 'GET',
            headers: getHeaders(),
            success: function (json) {
                self.setCurrentUser();
                var account = json.account;
                self.chosenAccountInfo(account);
                self.chosenAccountInfoData(account);
                var transactions = json.transactions;

                var mappedTransactions = $.map(transactions, function (item) { return new Transaction(item) });
                self.transactions(mappedTransactions);
                var users = json.users;
                var mappedUsers = $.map(users, function (item) { return new User(item) });
                self.accountUsers(mappedUsers)

                var amount = 0;
                transactions.forEach(function (t) {
                    amount += t.amount;
                }, this);
                self.accountSummary(amount);
            }
        });
    })
    this.get('#/account/edit/:id', function(context) {
        self.chosenHome(null);
        self.chosenAddAccount(null);
        self.chosenAddTransaction(null);
        self.chosenAddSharedAccount(null);
        self.chosenAccountInfo(null);
        self.chosenEditAccount({
            id: self.chosenAccountInfoData().id,
            name: self.chosenAccountInfoData().name
        });
    })
    this.get('#/transaction/:id', function(context) {
      self.chosenHome(null);
      self.chosenAddAccount(null);
      self.chosenAccountInfo(null);
      self.chosenAddSharedAccount(null);
      self.chosenEditAccount(null);
      self.chosenAddTransaction('transaction');
    })
  }).run("#/");
}

ko.applyBindings(new AppViewModel());
});
