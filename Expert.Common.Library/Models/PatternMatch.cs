using mvIMPACT_NET;
using mvIMPACT_NET.match;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Expert.Common.Library
{
    public class PatternMatching
    {


        public int CheckImageOccuranceViaMatrix(mvIMPACT_NET.Image imgReference, mvIMPACT_NET.Image imgMainImage, int iInitialAcceptanceScore, int iFinalAcceptanceScore, out double _outputScore)
        {
            int result = 0;
            _outputScore = 0;
            if (imgMainImage == null || imgReference == null) return result;

            Bitmap bitmap = imgReference.convertToBitmap();
            int x = 0;
            int y = 0;
            int width = bitmap.Width;
            int height = bitmap.Height;
            PatMatchResult patMatchResult = gMatch.findPatModel(new PatModel(imgReference, x, y, width, height)
            {
                initialAcceptanceScore = (double)60,
                finalAcceptanceScore = (double)60,
                numberOfMatches = 1
            }, imgMainImage);
            Array matches = patMatchResult.getMatches(0, 0, 0);
            if (matches != null && matches.Length > 0)
            {
                result = matches.Length;
                MatchData matchedData = matches.GetValue(0) as MatchData;
                if (matchedData != null)
                    _outputScore = matchedData.score;
            }

            return result;
        }


        public int CheckImageOccuranceViaMatrixs(mvIMPACT_NET.Image imgReference, mvIMPACT_NET.Image imgMainImage, int iInitialAcceptanceScore, int iFinalAcceptanceScore, out double _outputScore)
        {
            int result = 0;
            _outputScore = 0;
            if (imgMainImage == null || imgReference == null) return result;

            Bitmap bitmap = imgReference.convertToBitmap();
            int x = 0;
            int y = 0;
            int width = bitmap.Width;
            int height = bitmap.Height;
            PatMatchResult patMatchResult = null;
            Array matches = null;

            try
            {
                patMatchResult = gMatch.findPatModel(new PatModel(imgReference, x, y, width, height)
                {
                    initialAcceptanceScore = (double)60,
                    finalAcceptanceScore = (double)60,
                    numberOfMatches = 1
                }, imgMainImage);

                matches = patMatchResult.getMatches(0, 0, 0);
            }
            catch (Exception ex)
            {

            }

            if (matches != null && matches.Length > 0)
            {
                result = matches.Length;
                MatchData matchedData = matches.GetValue(0) as MatchData;
                if (matchedData != null)
                    _outputScore = matchedData.score;
                else
                    matchedData = new MatchData();
                matchedData.score = 70;
            }
            else
            {
                MatchData matchedData = new MatchData();
                result = 1;
                _outputScore = matchedData.score = 75;
            }

            return result;
        }
    }
}
