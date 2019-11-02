/**/
var mainDashboard = document.querySelector('.dashboard-side-nav-collapse');

mainDashboard.addEventListener('mouseout', (event) => {
  //event.preventDefault();
  console.log('Looooooool');
  //toggleDashboardMenu();
});


var menuToggle = document.querySelector('.hamburger-menu-toggle');
menuToggle.addEventListener('click', toggleDashboardMenu);



function toggleDashboardMenu() {

    if ((mainDashboard.classList.contains('show-menu') == false) && (mainDashboard.classList.contains('hide-menu') == false)) {
        console.log('Show menu and Hide are off. Turning appropriate menu.');
        if (mainDashboard.offsetWidth == 0) {
            console.log(mainDashboard.offsetWidth);
            console.log('Menu will now show');
            mainDashboard.classList.toggle('show-menu');
        } else {
            console.log('Menu will now be hidden.')
            console.log(mainDashboard.offsetWidth);

            mainDashboard.classList.toggle('hide-menu');
        }
    } else if (mainDashboard.classList.contains('show-menu') == true) {
        
        mainDashboard.classList.toggle('hide-menu');
        mainDashboard.classList.toggle('show-menu');
        console.log('Show menu is on.Now hiding...');

    } else if (mainDashboard.classList.contains('hide-menu') == true) {
      
        mainDashboard.classList.toggle('hide-menu');
        mainDashboard.classList.toggle('show-menu');
        console.log('Hide menu is on.Now Showing...');
    }

    console.log('toggle activated.')
  mainDashboard.focus();
}