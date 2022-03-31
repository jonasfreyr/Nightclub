using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMinigame : MonoBehaviour
{
    public List<Color> _colors = new List<Color>();
    public List<Wire> _leftWires = new List<Wire>();
    public List<Wire> _rightWires = new List<Wire>();

    private List<Color> _availableColors;
    private List<int> _availableLeftIndex;
    private List<int> _availableRightIndex;

    private void Start() {
        _availableColors = new List<Color>(_colors);
        _availableLeftIndex = new List<int>();
        _availableRightIndex = new List<int>();

        for (int i=0; i < _leftWires.Count; i++) {_availableLeftIndex.Add(i);}
        for (int i=0; i < _rightWires.Count; i++) {_availableLeftIndex.Add(i);}

        while (_availableColors.Count > 0 && _availableLeftIndex.Count > 0 && _availableRightIndex.Count > 0) {
            Color pickedColor = _availableColors[Random.Range(0, _availableColors.Count)];
            int pickedLeftWire = Random.Range(0, _availableLeftIndex.Count);
            int pickedRightWire = Random.Range(0, _availableRightIndex.Count);

            _leftWires[_availableLeftIndex[pickedLeftWire]].SetColor(pickedColor);
            _rightWires[_availableRightIndex[pickedRightWire]].SetColor(pickedColor);

            _availableColors.Remove(pickedColor);
            _availableLeftIndex.RemoveAt(pickedLeftWire);
            _availableRightIndex.RemoveAt(pickedRightWire);
        }
    }

}
