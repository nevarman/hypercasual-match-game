using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HyperCasualMatchGame.UI
{
    public class ScoreUIModel : WatchedVariable<int>{

    }
    public class ScoreUIController
    {
        ScoreUIModel _model;
        ScoreUI _view;

        public ScoreUIController(ScoreUIModel model, ScoreUI view)
        {
            _model = model;
            _view = view;

            _model.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(int obj)
        {
            _view.UpdateUI(obj);
        }
    }
}