/**/
var menuToggle = document.querySelector('.hamburger-menu-toggle');
menuToggle.addEventListener('click',toggleDashboardMenu)


function toggleDashboardMenu()
{
  var dashboardMenu = document.querySelector('.dashboard-side-nav-collapse');
  
  if((dashboardMenu.classList.contains('show-menu') == false) && (dashboardMenu.classList.contains('.hide-menu') == false))
  {
    
    //dashboardMenu.classList.toggle('toggle-on');
    console.log('Show menu and Hide are off. Turning appropriate menu.');
    if(dashboardMenu.style.width <= 0)
    {
          console.log('Menu will now show');
          dashboardMenu.classList.toggle('show-menu');
    }
    else
    {
         console.log('Menu will now be hidden.')
          dashboardMenu.classList.toggle('hide-menu');
    }
  }
  
  else if(dashboardMenu.classList.contains('show-menu') == true)
  {
    dashboardMenu.classList.toggle('hide-menu');
    //dashboardMenu.classList.toggle('show-menu');
    console.log('Show menu is on.Now hiding...');

  }

  else if(dashboardMenu.classList.contains('hide-menu') == true)
  {
    //dashboardMenu.classList.toggle('hide-menu');
    dashboardMenu.classList.toggle('show-menu');
    console.log('Hide menu is on.Now Showing...');
  }

  console.log('toggle activated.')
}

