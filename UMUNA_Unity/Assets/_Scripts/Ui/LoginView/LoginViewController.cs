using System.Collections.Generic;
using UMUNA.Data.DTOs;
using UnityEngine;
using UnityEngine.UIElements;

namespace UMUNA
{
    public class LoginViewController : MonoBehaviour
    {
        private Label _resultLabel;
        private ListView _userList;
        private TextField _passwordField;
        private Button _loginButton;

        private List<UserDataDto> _users = new();


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
