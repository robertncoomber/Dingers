using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rob.VR
{
    public class onHover : UnityEvent<int>{}
    public class onClick : UnityEvent<int>{}

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class IP_VR_RadialMenu : MonoBehaviour 
    {
        #region Variables
        [Header("Controller Properties")]
        public SteamVR_TrackedController controller;

        [Header("UI Properties")]
        public List<MenuGroup> menuGroups = new List<MenuGroup>();
        //public List<IP_VR_MenuButton> menuButtons = new List<IP_VR_MenuButton>();
        public RectTransform m_ArrowContainer;
        public Text m_DebugText;

        [Header("Events")]
        public UnityEvent OnMenuChanged = new UnityEvent();


        private Vector2 currentAxis;
        private SteamVR_Controller.Device controllerDevice;
        private Animator animator;

        private bool menuOpen = false;
        private bool allowNavigation = false;
        private bool isTouching = false;
        private float currentAngle;

        private int currentMenuID = -1;
        private int previousMenuID = -1;

        private onHover OnHover = new onHover();
        private onClick OnClick = new onClick();
        #endregion


        #region Main Methods
    	// Use this for initialization
    	void Start () 
        {
            animator = GetComponent<Animator>();

            if(controller)
            {
                controllerDevice = SteamVR_Controller.Input((int)controller.controllerIndex);
                controller.PadTouched += HandlePadTouched;
                controller.PadUntouched += HandlePadUnTouched;
                controller.PadClicked += HandlePadClicked;
                controller.MenuButtonClicked += HandleMenuActivation;
            }

            
            for(int i = 0; i <menuGroups.Count; i++)
            {
                if (menuGroups[i].menuButtons.Count > 0)
                {
                    foreach (var button in menuGroups[i].menuButtons)
                    {
                        OnHover.AddListener(button.Hover);
                        OnClick.AddListener(button.Click);
                    }
                }
            }
    	}

        void OnDisable()
        {
            if(controller)
            {
                controller.PadTouched -= HandlePadTouched;
                controller.PadUntouched -= HandlePadUnTouched;
                controller.PadClicked -= HandlePadClicked;
                controller.MenuButtonClicked -= HandleMenuActivation;
            }

            if(OnHover != null)
            {
                OnHover.RemoveAllListeners();
            }

            if(OnClick != null)
            {
                OnClick.RemoveAllListeners();
            }
        }

    	void Update () 
        {
            if(controllerDevice != null)
            {
                if(menuOpen && isTouching)
                {
                    UpdateMenu();
                }
            }
    	}
        #endregion


        #region Custom Methods
        void HandlePadTouched(object sender, ClickedEventArgs e)
        {
            isTouching = true;
        }

        void HandlePadUnTouched(object sender, ClickedEventArgs e)
        {
            isTouching = false;
        }

        void HandlePadClicked(object sender, ClickedEventArgs e)
        {
            if (!menuOpen) // open main menu
            {
                Game.isPlaying = false;
                menuOpen = true;
                HandleAnimator();
            }
            else if (OnClick != null)
            {
                OnClick.Invoke(currentMenuID);
                
                if(currentMenuID == 0 || currentMenuID == 3) // checks if chosen button cause menu fade
                {
                    menuOpen = false;
                    HandleAnimator();
                }
            }
        }
        
        // This piece of code detects when the menu button is clicked.
        // It sets is open to true and pauses the game. It also runs the
        // HandleAnimator function to run the animation.
        void HandleMenuActivation(object sender, ClickedEventArgs e)
        {
            
        }

        void HandleAnimator()
        {
            if(animator)
            {
                animator.SetBool("open", menuOpen);
            }
        }

        // In update if is touching is true, this is ran once per update
        // It takes the touch angle and coverts it to the int updateMenuID
        void UpdateMenu()
        {
            if(isTouching)
            {
                //Get the Current Axis from the Touch Pad and turn it into and Angle
                currentAxis = controllerDevice.GetAxis();
                currentAngle = Vector2.SignedAngle(Vector2.up, currentAxis);
                
                float menuAngle = currentAngle;
                if(menuAngle < 0)
                {
                    menuAngle += 360f;
                }
                int updateMenuID = (int)(menuAngle / (360f / 4f));

                //Update Current Menu ID
                if(updateMenuID != currentMenuID)
                {
                    if(OnHover != null)
                    {
                        OnHover.Invoke(updateMenuID);    
                    }

                    if(OnMenuChanged != null)
                    {
                        OnMenuChanged.Invoke();
                    }

                    previousMenuID = currentMenuID;
                    currentMenuID = updateMenuID;
                }


                //Rotate Arrow
                if(m_ArrowContainer)
                {
                    m_ArrowContainer.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
                }
            }
        }

        void HandleDebugText(string aString)
        {
            if(m_DebugText)
            {
                //m_DebugText.text = aString;
            }
        }
        #endregion
    }
}
