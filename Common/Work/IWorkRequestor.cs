﻿/* Copyright 2011 the OpenDMS.NET Project (http://sites.google.com/site/opendmsnet/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Common.Work
{
    /// <summary>
    /// Represents an interface used to indicate the inheriting class can request jobs of the <see cref="Master"/>.
    /// </summary>
    public interface IWorkRequestor
    {
        /// <summary>
        /// WorkReport accepts handles pumping this message to the UI
        /// </summary>
        /// <param name="result">The <see cref="JobResult"/>.</param>
        void WorkReport(JobResult result);
    }
}