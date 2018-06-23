using CodeStack.Community.DevTools.Sw.Testing.TempDisplay;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.Services
{
    public enum EntityType_e
    {
        swSelFACES = swSelectType_e.swSelFACES,
        swSelEDGES = swSelectType_e.swSelEDGES,
        swSelVERTICES = swSelectType_e.swSelVERTICES
    }

    public class DocumentService
    {
        public TEnt GetEntity<TEnt>(IPartDoc part, string name, EntityType_e type)
            where TEnt : class
        {
            if (part == null)
            {
                throw new ArgumentNullException(nameof(part));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return part.GetEntityByName(name, (int)type) as TEnt;
        }

        public void TempDisplayPoints(IModelDoc2 model, IEnumerable<double[]> points,
            IDisposeToken tempDisposeToken = null)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            var activeSketch = model.SketchManager.ActiveSketch;

            if (activeSketch != null)
            {
                InsertSketch(model, activeSketch.Is3D());
            }

            model.ClearSelection2(true);

            InsertSketch(model, true);

            var tempSketchFeat = model.SketchManager.ActiveSketch as IFeature;

            foreach (var pt in points)
            {
                model.SketchManager.CreatePoint(pt[0], pt[1], pt[2]);
            }

            TempDispose(tempDisposeToken, () => 
            {
                tempSketchFeat.Select2(false, -1);
                model.Extension.DeleteSelection2((int)swDeleteSelectionOptions_e.swDelete_Absorbed);

                if (activeSketch != null)
                {
                    (activeSketch as IFeature).Select2(false, -1);
                    InsertSketch(model, activeSketch.Is3D());
                }
            });
        }

        public void TempDisplayBody(IBody2 body, IModelDoc2 model, IDisposeToken tempDisposeToken = null)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (!body.IsTemporaryBody())
            {
                throw new ArgumentException("Only temp bodies are supported");
            }

            const int COLORREF_YELLOW = 65535;

            body.Display3(model, COLORREF_YELLOW,
                (int)swTempBodySelectOptions_e.swTempBodySelectOptionNone);

            body.MaterialPropertyValues2 = new double[] { 1, 1, 0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5 };

            TempDispose(tempDisposeToken, () => body.Hide(model));
        }

        private void TempDispose(IDisposeToken tempDisposeToken, DisposeFunction disposeFunc)
        {
            if (tempDisposeToken != null)
            {
                tempDisposeToken.DisposePreview += disposeFunc;
                tempDisposeToken.Wait();
            }
            else
            {
                disposeFunc.Invoke();
            }
        }

        private void InsertSketch(IModelDoc2 model, bool is3D)
        {
            if (is3D)
            {
                model.Insert3DSketch2(true);
            }
            else
            {
                model.InsertSketch2(true);
            }
        }
    }
}
