/**/
var menuToggle = document.querySelector('.hamburger-menu');
menuToggle.addEventListener('click', toggleDashboardMenu);


function toggleDashboardMenu() {
    var dashboardMenu = document.querySelector('.dashboard-side-nav-collapse');

    if ((dashboardMenu.classList.contains('show-menu') == false) && (dashboardMenu.classList.contains('hide-menu') == false)) {
        console.log('Show menu and Hide are off. Turning appropriate menu.');
        if (dashboardMenu.offsetWidth == 0) {
            console.log(dashboardMenu.offsetWidth);
            console.log('Menu will now show');
            dashboardMenu.classList.toggle('show-menu');
        } else {
            console.log('Menu will now be hidden.')
            console.log(dashboardMenu.offsetWidth);

            dashboardMenu.classList.toggle('hide-menu');
        }
    } else if (dashboardMenu.classList.contains('show-menu') == true) {
        
        dashboardMenu.classList.toggle('hide-menu');
        dashboardMenu.classList.toggle('show-menu');
        console.log('Show menu is on.Now hiding...');

    } else if (dashboardMenu.classList.contains('hide-menu') == true) {
      
        dashboardMenu.classList.toggle('hide-menu');
        dashboardMenu.classList.toggle('show-menu');
        console.log('Hide menu is on.Now Showing...');
    }

    console.log('toggle activated.')
}

function expandSubmenu()
{
    // 1. Get reference to parent submenu.
    let parentSubMenues = document.querySelectorAll('.js-submenu-parent');

    parentSubMenues.forEach(function (parentSubMenu) {
        parentSubMenu.addEventListener('click', function () {
            //alert('Submenu Click.');
            //3. Find child node.
            console.log(parentSubMenu.nextElementSibling);
            let submenu = parentSubMenu.nextElementSibling;
            console.log(submenu);
            parentSubMenu.classList.toggle('menu-expanded');
            submenu.classList.toggle('visible')

           //4. Display the child to block. 

           // 5. Make parent active/on


        });
    });
}

function activateMenu()
{
    var profileNav = document.querySelector('.profile-submenu-nav');
    profileNav.addEventListener('click', function ()
    {
        // 1. Toggle class to show active.
        profileNav.classList.toggle('selected');


    }, null);

}
activateMenu();
expandSubmenu();