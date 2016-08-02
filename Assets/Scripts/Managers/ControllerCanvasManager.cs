﻿/*
 * Copyright 2015 Google Inc. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System.Collections;

using Google.Cast.RemoteDisplay;

namespace CompleteProject {
  public class ControllerCanvasManager : MonoBehaviour {
    private GameObject playOnTvButton;

    public GameObject backgroundImage;

    public void Start() {
      CastRemoteDisplayManager displayManager = CastRemoteDisplayManager.GetInstance();
      GameObject buttonFrame = GameObject.Find("CastButtonFrame");
      playOnTvButton = buttonFrame.transform.Find("PlayOnTvButton").gameObject;
      backgroundImage.SetActive(displayManager.IsCasting());
      playOnTvButton.SetActive(!displayManager.IsCasting());

      displayManager.RemoteDisplaySessionStartEvent.AddListener(OnRemoteDisplaySessionStart);
      displayManager.RemoteDisplaySessionEndEvent.AddListener(OnRemoteDisplaySessionEnd);
    }

    private void OnDestroy() {
      CastRemoteDisplayManager displayManager = CastRemoteDisplayManager.GetInstance();
      displayManager.RemoteDisplaySessionStartEvent.RemoveListener(OnRemoteDisplaySessionStart);
      displayManager.RemoteDisplaySessionEndEvent.RemoveListener(OnRemoteDisplaySessionEnd);
    }

    private void OnRemoteDisplaySessionStart(CastRemoteDisplayManager manager) {
      backgroundImage.SetActive(true);
      playOnTvButton.SetActive(false);
    }

    private void OnRemoteDisplaySessionEnd(CastRemoteDisplayManager manager) {
      backgroundImage.SetActive(false);
      playOnTvButton.SetActive(true);
    }
  }
}
