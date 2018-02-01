# Copyright 2015 The Meson development team

# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at

#     http://www.apache.org/licenses/LICENSE-2.0

# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

from .. import mlog
from .qt import QtBaseModule
from . import ExtensionModule


class Qt5Module(ExtensionModule, QtBaseModule):

    def __init__(self):
        QtBaseModule.__init__(self, qt_version=5)
        ExtensionModule.__init__(self)

def initialize():
    mlog.warning('rcc dependencies will not work reliably until this upstream issue is fixed:',
                 mlog.bold('https://bugreports.qt.io/browse/QTBUG-45460'))
    return Qt5Module()
