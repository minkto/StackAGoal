(function dashboard() {


    function initDashboard() {

        initDashboardMenuToggle();
        initSelectedMenu();
        initExpandDropdownMenu();
        initResponsiveMenuOption();
        initOutsideClick();
    }

    function initExpandDropdownMenu() {
        let dropdownMenues = document.querySelectorAll('.js-nav-dropdown-menu');
        dropdownMenues.forEach(function (dropdownMenu) {
            dropdownMenu.addEventListener('click', function () {
                let submenu = dropdownMenu.parentElement.nextElementSibling;
                dropdownMenu.classList.toggle('menu-expanded');
                submenu.classList.toggle('visible');
            });
        });
    }

    function initSelectedMenu() {
        var menuOption = document.querySelectorAll('.js-nav-menu-icon');
        menuOption.forEach(function (el) {
            el.addEventListener('click', function () {
                el.parentElement.classList.toggle('selected');
            }, null);
        });
    }

    function initOutsideClick() {
        document.addEventListener('click', function (e) {
            var allMenuOptions = document.querySelectorAll('.js-nav-menu-icon');
            for (let i = 0; i < allMenuOptions.length; i++) {
                var isClickInside = allMenuOptions[i].contains(event.target);
                
                if (!isClickInside) {
                    if (allMenuOptions[i].parentElement.classList.contains('selected')) {
                        allMenuOptions[i].parentElement.classList.toggle('selected');
                    }
                }
            }
        });
    }

    function initResponsiveMenuOption() {
        var iconMenu = document.querySelector('.icon-menu-button');
        var iconMenuContainer = document.querySelector('.menu-options-container');

        iconMenu.addEventListener('click', function () {
            iconMenu.parentElement.classList.toggle('selected');
            iconMenuContainer.classList.toggle('show');

        }, null);
    }

    function initDashboardMenuToggle() {
        var dashboardMenu = document.querySelector('.dashboard-side-nav-collapse');
        var menuToggle = document.querySelector('#nav-button-hamburger');
        var hamburgerMenuContainer = document.querySelector('.hamburger-menu');

        
        menuToggle.addEventListener('click', function () {

            if ((dashboardMenu.classList.contains('show-menu') == false) && (dashboardMenu.classList.contains('hide-menu') == false)) {
                console.log('Show menu and Hide are off. Turning appropriate menu.');
                if (dashboardMenu.offsetWidth == 0)
                {
                    dashboardMenu.classList.toggle('show-menu');
                    hamburgerMenuContainer.classList.toggle('show-menu');
                } else
                {
                    dashboardMenu.classList.toggle('hide-menu');
                }
            } else if ((dashboardMenu.classList.contains('show-menu') == true) || (dashboardMenu.classList.contains('hide-menu') == true)) {

                dashboardMenu.classList.toggle('hide-menu');
                dashboardMenu.classList.toggle('show-menu');
                hamburgerMenuContainer.classList.toggle('show-menu');
            }

        }, null);       
    }

    // Initialise the dashboard and navigation menues.
    initDashboard();

})();

